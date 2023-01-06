using AlecEdu_api.API.Common;
using AlecEdu_api.Application.Features.V1.Users.Queries.GetUsers;
using AlecEdu_api.Application.Models;
using AlecEdu_api.Domain.Common;
using AlecEdu_api.Domain.Entities;
using AlecEdu_api.Domain.Enums;
using AlecEdu_api.Domain.Modules;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlecEdu_api.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]

public class UsersController : BaseApiController
{
    private readonly ILogger<UsersController> _logger;
    private readonly IMediator _mediator;

    public UsersController(ILogger<UsersController> logger,  IMediator mediator) : base(logger, mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }
    
    [HttpGet]
    // [AuthorizeMultiplePolicy($"{nameof(ETypeUser.Develop)}",$"{UserModule.Name}{nameof(EActionModule.List)}")]

    [Authorize(Policy = $"{UserModule.Name}{nameof(EActionModule.List)}")]
    public async Task<IActionResult> GetList([FromQuery] FilterBase<User> filters)
    {
        _logger.LogInformation("UsersController@GetList -- Start, data: {0} {1} {2}", filters,
            typeof(UserDto), typeof(User));
        var getListQuery = new GetUserQuery() { Filter = filters };
        var response = await _mediator.Send(getListQuery);
        _logger.LogInformation("UsersController@GetList -- End");
        return Ok(response);
    }
}
