using AlecEdu_api.Application.Common;
using AlecEdu_api.Application.Common.Interfaces.Authentication;
using AlecEdu_api.Application.Models;
using AlecEdu_api.Domain.Common;
using AlecEdu_api.Domain.Entities;
using AlecEdu_api.Domain.Enums;
using IdentityServer4.Stores;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace AlecEdu_api.Application.Features.V1.Users.Commands.Login;

public class LoginCommand: IRequest<BaseResponse<TokenResponseDto>>
{
    public UserLoginDto Data { get; init; } = null!;
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, BaseResponse<TokenResponseDto>>
{
    private readonly ILogger<LoginCommandHandler> _logger;
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenGenerator _generator;
    
    public LoginCommandHandler(
        ILogger<LoginCommandHandler> logger,
        IUnitOfWork unitOfWork,
        UserManager<User> userManager,
        IJwtTokenGenerator generator,
        IRefreshTokenStore refreshTokenStore
    )
    {
        _logger = logger;
        _userManager = userManager;
        _generator = generator;
    }
    
    public async Task<BaseResponse<TokenResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("LoginCommandHandler@Handle -- Start");
        var response = new BaseResponse<TokenResponseDto>(
            success: false
            );
        var validator = new LoginCommandValidator();
        var validationResult = await validator.ValidateAsync(request.Data, cancellationToken);
        
        if (validationResult.Errors.Count > 0)
        {
            _logger.LogInformation("LoginCommandHandler@Handle -- End -- Validate");
            throw new Exceptions.ValidationException(validationResult);
        }
        
        var user = await _userManager.FindByNameAsync(request.Data.UserName);
        if (await _userManager.CheckPasswordAsync(user, request.Data.Password))
        {
            var token = await _generator.GenerateTokenByIdentityServer4(user, request.Data.Password);
            _logger.LogInformation("LoginCommandHandler@Handle -- End");
            response.Data = token;
            response.ResultCode = EResultCode.CREATE;
            response.Success = true;
            return response;
        }
        response.ResultCode = EResultCode.BADREQUEST;
        response.Message = "Thong tin dang nhap khong dung";
        return response;
    }
}