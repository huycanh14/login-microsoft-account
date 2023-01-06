using System.Reflection;
using AlecEdu_api.API.Common;
using AlecEdu_api.Application.Features.V1.Users.Commands.Login;
using AlecEdu_api.Application.Models;
using AlecEdu_api.Domain.Common;
using AlecEdu_api.Domain.Enums;
using AlecEdu_api.Domain.Modules;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AlecEdu_api.API.Controllers;

public class AuthController : BaseApiController
{
    private readonly ILogger<AuthController> _logger;
    private readonly IMediator _mediator;
    private readonly GoogleSettings _googleSettings;

    public AuthController(
        ILogger<AuthController> logger,
        IMediator mediator,
        IOptions<GoogleSettings> googleSettings
        ): base(logger, mediator)
    {
        _logger = logger;
        _mediator = mediator;
        _googleSettings = googleSettings.Value;
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
    // public List<Dictionary<EActionModule, string>> Demo()
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
    
        // return listString;
        return listDmo;
    }

}
