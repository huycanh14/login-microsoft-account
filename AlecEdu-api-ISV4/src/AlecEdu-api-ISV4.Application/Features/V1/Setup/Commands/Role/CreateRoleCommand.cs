using AlecEdu_api.Application.Features.V1.Users.Commands.Login;
using AlecEdu_api.Application.Models;
using AlecEdu_api.Domain.Common;
using AlecEdu_api.Domain.Entities;
using AlecEdu_api.Domain.Enums;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace AlecEdu_api.Application.Features.V1.Setup.Commands.Role;

public class CreateRoleCommand: IRequest<BaseResponse<RoleDto>>
{
    public RoleDto Data { get; init; } = null!;
}

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, BaseResponse<RoleDto>>
{
    private readonly ILogger<LoginCommandHandler> _logger;
    private readonly  RoleManager<IdentityRole<int>> _roleManager;
    private readonly  UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public CreateRoleCommandHandler(
        ILogger<LoginCommandHandler> logger,
        RoleManager<IdentityRole<int>> roleManager,
        UserManager<User> userManager,
        IMapper mapper
        )
    {
        _logger = logger;
        _roleManager = roleManager;
        _userManager = userManager;
        _mapper = mapper;
    }
    public async Task<BaseResponse<RoleDto>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CreateRoleCommandHandler -- Start");
        var response = new BaseResponse<RoleDto>(
            success: false
        );
        var roleExist = await _roleManager.RoleExistsAsync(request.Data.Name);
        if (!roleExist) {
            var role = _mapper.Map<IdentityRole<int>>(request.Data);
            var roleResult = await _roleManager.CreateAsync(role);

            if (roleResult.Succeeded) {
                _logger.LogInformation (1, "Roles Added");
                response.ResultCode = EResultCode.CREATE;
                response.Data = request.Data;
                response.Success = true;
            } else {
                response.ResultCode = EResultCode.BADREQUEST;
                response.Message = $"Issue adding the new {request.Data.Name} role";
            }
        }
        else
        {
            response.ResultCode = EResultCode.BADREQUEST;
            response.Message = $"{request.Data.Name} role is exist";
        }

        return response;
    }
}