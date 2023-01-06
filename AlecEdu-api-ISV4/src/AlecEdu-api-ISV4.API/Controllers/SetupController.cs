using System.Security.Claims;
using AlecEdu_api.API.Common;
using AlecEdu_api.Application.Features.V1.Setup.Commands.Role;
using AlecEdu_api.Application.Models;
using AlecEdu_api.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AlecEdu_api.API.Controllers;

public class SetupController: BaseApiController
{
    private readonly ILogger<SetupController> _logger;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly  UserManager<User> _userManager;
    private readonly  RoleManager<IdentityRole<int>> _roleManager;
    

    public SetupController(
        ILogger<SetupController> logger,
        IMapper mapper,
        UserManager<User> userManager,
        RoleManager<IdentityRole<int>> roleManager,
        IMediator mediator
        ) : base(logger, mediator)
    {
        _logger = logger;
        _mediator = mediator;
        _mapper = mapper;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    
    [HttpPost("role")]
    public async Task<IActionResult> CreateRole(RoleDto role)
    {
        var tokenCommand = new CreateRoleCommand() {  Data = role };
        var response = await _mediator.Send(tokenCommand);

        return Ok(response);
    }

    [HttpPost("role_claim")]
    public async Task<IActionResult> CreateRoleClaim()
    {
        var adminRole = await _roleManager.FindByNameAsync("Administrator");
        await _roleManager.AddClaimAsync(adminRole, new Claim("users", "users.list"));
        // await _roleManager.AddClaimAsync(adminRole, new Claim("users", "projects.create"));
        // await _roleManager.AddClaimAsync(adminRole, new Claim("users", "projects.update"));
        return Ok();
    }
}
