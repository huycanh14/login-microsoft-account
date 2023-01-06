using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AlecEdu_api.Application.Common.Interfaces.Authentication;
using AlecEdu_api.Application.Common.Interfaces.Services;
using AlecEdu_api.Application.Models;
using AlecEdu_api.Domain.Common;
using AlecEdu_api.Domain.Entities;
using AlecEdu_api.Domain.Enums;
using AlecEdu_api.Infrastructure.Persistence;
using AutoMapper;
using IdentityModel.Client;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;
using Token = AlecEdu_api.Domain.Entities.Token;

namespace AlecEdu_api.Infrastructure.Authentication;

public class JwtTokenGenerator: IJwtTokenGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly JwtSettings _jwtSettings;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AlecEduContext _context;
    private readonly IRefreshTokenStore _refreshTokenStore;
    
    public JwtTokenGenerator(
        IDateTimeProvider dateTimeProvider, 
        IOptions<JwtSettings> jwSettings,
        UserManager<User> userManager,
        IMapper mapper,
        RoleManager<IdentityRole<int>> roleManager,
        IHttpContextAccessor httpContextAccessor,
        AlecEduContext context,
        IRefreshTokenStore refreshTokenStore
        )
    {
        _dateTimeProvider = dateTimeProvider;
        _jwtSettings = jwSettings.Value;
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _context = context;
        _refreshTokenStore = refreshTokenStore;
    }
    
    public async Task<string> GenerateAccessToken(User user, DateTime? expireDays)
    {
        var userRoles = await _userManager.GetRolesAsync(user);
        
        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        switch (user.LoaiTaiKhoan)
        {
            case ETypeUser.SuperAdmin:
                authClaims.Add(new Claim(ClaimTypes.Role, nameof(ETypeUser.SuperAdmin)));
                break;
            case ETypeUser.Develop:
                authClaims.Add(new Claim(ClaimTypes.Role, nameof(ETypeUser.Develop)));
                break;
            default:
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    var role = await _roleManager.FindByNameAsync(userRole);

                    if (role == null) continue;
                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    authClaims.AddRange(roleClaims);
                }
                break;
        }
        var token = GetToken(authClaims, expireDays);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<string> GenerateRefreshToken(User user)
    {
        var token  = await _userManager
            .GenerateUserTokenAsync(user, _jwtSettings.RefreshTokenProviderName, _jwtSettings.RefreshTokenPurpose);
        return token;
    }

    public async Task<LoginResultDto> GenerateToken(User user)
    {
        var expireDays = _dateTimeProvider.UtcNow.AddSeconds(_jwtSettings.AccessExpireSeconds);
        var result = new LoginResultDto
        {
            Expires = expireDays,
            ExpiresIn = _jwtSettings.AccessExpireSeconds,
            AccessToken = await GenerateAccessToken(user, expireDays),
            RefreshToken = await GenerateRefreshToken(user)
        };
        return result;
    }

    public async Task<TokenResponseDto> GenerateTokenByIdentityServer4(User user, string password)
    {
        var client = new HttpClient();
        var addresses = "";
        if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.Request.IsHttps)
        {
            addresses += "https://";
        }
        else
        {
            addresses += "http://";
        }
        addresses += _httpContextAccessor.HttpContext?.Request.Host.Value;

        var disco = await client.GetDiscoveryDocumentAsync(addresses);
        var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = "alec_edu_api_internal",
            ClientSecret = "secret",
            Scope = "full_access offline_access",
            UserName = user.UserName,
            Password = password,
            GrantType = "refresh_token",
        });
        
        var tokenDto =  _mapper.Map<TokenResponseDto>(tokenResponse);
        await SaveToken(tokenDto, null);
        return tokenDto;
    }

    public async Task<TokenResponseDto> GenerateTokenByIdentityServer4(User user)
    {
        var client = new HttpClient();
        var addresses = "";
        if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.Request.IsHttps)
        {
            addresses += "https://";
        }
        else
        {
            addresses += "http://";
        }
        addresses += _httpContextAccessor.HttpContext?.Request.Host.Value;

        var disco = await client.GetDiscoveryDocumentAsync(addresses);
        var tokenResponse = await client.RequestTokenAsync(new TokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = "alec_edu_api_internal",
            ClientSecret = "secret",
            Parameters =
            {
                { "email", user.Email },
            },
            GrantType = "login_external_sso",
        });
        
        var tokenDto =  _mapper.Map<TokenResponseDto>(tokenResponse);
        await SaveToken(tokenDto, null);
        return tokenDto;
    }

    public async Task SaveToken(TokenResponseDto tokenDto, string? refreshTokenOld)
    {
        await _context.Token.Where(x => x.RefreshToken.Equals(refreshTokenOld)).ExecuteDeleteAsync();
        var token = new Token();
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = tokenHandler.ReadJwtToken(tokenDto.AccessToken);
        tokenDto.ExpireDateTime = jwtSecurityToken.ValidTo;
        var refreshToken = await _refreshTokenStore.GetRefreshTokenAsync(tokenDto.RefreshToken);

        token.AccessToken = tokenDto.AccessToken;
        token.RefreshToken = tokenDto.RefreshToken;
        token.AccessTokenExpiresAt = tokenDto.ExpireDateTime;
        token.RefreshTokenExpiresAt = refreshToken.CreationTime.AddSeconds(refreshToken.Lifetime);
        token.UserId = int.Parse(jwtSecurityToken.Subject);
        token.CreatedAt = _dateTimeProvider.UtcNow;
        await _context.Token.AddAsync(token);

        await _context.SaveChangesAsync();
    }

    private JwtSecurityToken GetToken(IEnumerable<Claim> authClaims, DateTime? expireDays)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: expireDays ?? _dateTimeProvider.UtcNow.AddDays(_jwtSettings.AccessExpireSeconds),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );
        return token;
    }

    private static int ConvertDayToSecond(int day) => day * 86400;
}
