using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AlecEdu_api.Application.Common.Interfaces.Authentication;
using AlecEdu_api.Application.Common.Interfaces.Services;
using AlecEdu_api.Application.Models;
using AlecEdu_api.Domain.Common;
using AlecEdu_api.Domain.Entities;
using AlecEdu_api.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace AlecEdu_api.Infrastructure.Authentication;

public class JwtTokenGenerator: IJwtTokenGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly JwtSettings _jwtSettings;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;
    
    public JwtTokenGenerator(
        IDateTimeProvider dateTimeProvider, 
        IOptions<JwtSettings> jwSettings,
        UserManager<User> userManager,
        RoleManager<IdentityRole<int>> roleManager
        )
    {
        _dateTimeProvider = dateTimeProvider;
        _jwtSettings = jwSettings.Value;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    
    public async Task<string> GenerateAccessToken(User user, DateTime? accessExpireSeconds)
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
        var token = GetToken(authClaims, accessExpireSeconds);
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
        var accessExpireSeconds = _dateTimeProvider.UtcNow.AddDays(_jwtSettings.AccessExpireSeconds);
        var result = new LoginResultDto
        {
            Expires = accessExpireSeconds,
            ExpiresIn = ConvertDayToSecond(_jwtSettings.AccessExpireSeconds) - 2,
            AccessToken = await GenerateAccessToken(user, accessExpireSeconds),
            RefreshToken = await GenerateRefreshToken(user)
        };
        return result;
    }

    private JwtSecurityToken GetToken(IEnumerable<Claim> authClaims, DateTime? accessExpireSeconds)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: accessExpireSeconds ?? _dateTimeProvider.UtcNow.AddSeconds(_jwtSettings.AccessExpireSeconds),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );
        return token;
    }

    private static int ConvertDayToSecond(int day) => day * 86400;
}
