using AlecEdu_api.Application.Common;
using AlecEdu_api.Application.Common.Interfaces.Authentication;
using AlecEdu_api.Application.Models;
using AlecEdu_api.Domain.Common;
using AlecEdu_api.Domain.Entities;
using AlecEdu_api.Domain.Enums;
using AutoMapper;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AlecEdu_api.Application.Features.V1.Users.Commands.Login;

public class LoginWithGoogleCommand : IRequest<BaseResponse<TokenResponseDto>>
{
    public UserLoginExternalDto Data { get; init; } = null!;
}

public class LoginWithGoogleCommandHandler : IRequestHandler<LoginWithGoogleCommand, BaseResponse<TokenResponseDto>>
{
    private readonly ILogger<LoginCommandHandler> _logger;
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenGenerator _generator;
    private readonly GoogleSettings _googleSettings;
    private readonly JwtSettings _jwtSettings;
    private readonly IUnitOfWork _unitOfWork;

    public LoginWithGoogleCommandHandler(
        ILogger<LoginCommandHandler> logger,
        UserManager<User> userManager,
        IUnitOfWork unitOfWork,
        IOptions<GoogleSettings> googleSettings,
        IJwtTokenGenerator generator,
        IOptions<JwtSettings> jwSettings,
        IMapper mapper
    )
    {
        _logger = logger;
        _userManager = userManager;
        _generator = generator;
        _googleSettings = googleSettings.Value;
        _jwtSettings = jwSettings.Value;
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponse<TokenResponseDto>> Handle(
        LoginWithGoogleCommand request,
        CancellationToken cancellationToken
        )
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        _logger.LogInformation("LoginWithGoogleCommandHandler@Handle -- Start");
        var response = new BaseResponse<TokenResponseDto>(
            success: false
        );
        try
        {
            var validator = new LoginWithGoogleCommandValidator();
            var validationResult = await validator.ValidateAsync(request.Data, cancellationToken);

            if (validationResult.Errors.Count > 0)
            {
                _logger.LogInformation("LoginWithGoogleCommandHandler@Handle -- End -- Validate");
                throw new Exceptions.ValidationException(validationResult);
            }

            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { _googleSettings.ClientId }
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(request.Data.Token, settings);
            if (payload == null)
            {
                response.ResultCode = EResultCode.FORBIDDEN;
                response.Message = "Invalid External Authentication.";
                return response;
            }

            var user = await _userManager.FindByEmailAsync(payload.Email);
            if (user != null)
            {
                var token = await _generator.GenerateTokenByIdentityServer4(user);
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
        catch (Exception e)
        {
            response.ResultCode = EResultCode.FORBIDDEN;
            response.Message = e.Message; //"Invalid External Authentication.";
            return response;
        }
    }
}

