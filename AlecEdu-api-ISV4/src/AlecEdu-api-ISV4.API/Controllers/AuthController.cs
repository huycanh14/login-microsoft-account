using System.Reflection;
using AlecEdu_api.API.Common;
using AlecEdu_api.Application.Common.Interfaces.Authentication;
using AlecEdu_api.Application.Contracts.Persistence;
using AlecEdu_api.Application.Features.V1.Users.Commands.Login;
using AlecEdu_api.Application.Models;
using AlecEdu_api.Domain.Common;
using AlecEdu_api.Domain.Enums;
using AlecEdu_api.Domain.Modules;
using AutoMapper;
using IdentityModel.Client;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlecEdu_api.API.Controllers;

[ApiController]
public class AuthController : BaseApiController
{
    private readonly ILogger<AuthController> _logger;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly ITokenRepository _tokenRepository;
    public AuthController(
        ILogger<AuthController> logger, 
        IMediator mediator, 
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        IJwtTokenGenerator jwtTokenGenerator,
        ITokenRepository tokenRepository
        ): base(logger, mediator)
    {
        _logger = logger;
        _mediator = mediator;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _jwtTokenGenerator = jwtTokenGenerator;
        _tokenRepository = tokenRepository;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> LoginWithUserNameAndPass(UserLoginDto userLogin)
    {
        _logger.LogInformation("AuthController@GetList -- Start, data: {0} {1}",
            typeof(UserLoginDto), userLogin);
        var tokenCommand = new LoginCommand() {  Data = userLogin };
        var response = await _mediator.Send(tokenCommand);
        _logger.LogInformation("AuthController@GetList -- End");
        return Ok(response);
    }
    
    [HttpPost("login/external-google")] 
    public async Task<IActionResult> LoginGoogle(UserLoginExternalDto userLogin)
    {
        var tokenCommand = new LoginWithGoogleCommand() {  Data = userLogin };
        var response = await _mediator.Send(tokenCommand);
        _logger.LogInformation("AuthController@LoginGoogle -- End");
        return Ok(response);

    }

    [HttpPost("test")]
    public List<string> Demo()
    {
        var listString = new List<Dictionary<EActionModule, string>>();
        var listDmo = new List<string>();
        var exporters = typeof(BaseModule)
            .Assembly.GetTypes()
            .Where(t => t.IsSubclassOf(typeof(BaseModule)) && !t.IsAbstract)
            .Select(t => (BaseModule)Activator.CreateInstance(t));
        foreach (var x in exporters)
        {
            listString.Add(x.ClaimValues);
            foreach(KeyValuePair<EActionModule, string> entry in x.ClaimValues)
            {
                // do something with entry.Value or entry.Key
                listDmo.Add($"{x.PolicyName}{entry.Key}");
            }
        }
        return listDmo;
    }

    [HttpPost("check_refresh")]
    public async Task<IActionResult> CheckRefreshToken(string value)
    {
        var response = new BaseResponse<TokenResponseDto>(
                 success: false
             );
        var tokenDb = await _tokenRepository.FindByCondition(x => x.RefreshToken.Equals(value)).FirstOrDefaultAsync();
        if (tokenDb == null || tokenDb.RefreshTokenExpiresAt <= DateTime.UtcNow)
        {
            return Forbid();
        }
        
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
        var tokenClient = await client.RequestRefreshTokenAsync(new RefreshTokenRequest
        {
            Address = disco.TokenEndpoint,
            GrantType = "refresh_token",
            ClientId = "alec_edu_api_internal",
            ClientSecret = "secret",
            RefreshToken = value
        });
        if (tokenClient.IsError)
        {
            response.Message = tokenClient.Error;
            response.ResultCode = (EResultCode)tokenClient.HttpStatusCode;
        }
        else
        {
            var tokenDto =  _mapper.Map<TokenResponseDto>(tokenClient);
            await _jwtTokenGenerator.SaveToken(tokenDto, value);
            response.Success = true;
            response.ResultCode = EResultCode.CREATE;
            response.Data = tokenDto;
        }
        return Ok(response);
    }
}
