using AlecEdu_api.Application.Common;
using AlecEdu_api.Application.Common.Interfaces.Authentication;
using AlecEdu_api.Application.Contracts.Persistence;
using AlecEdu_api.Application.Models;
using AlecEdu_api.Domain.Common;
using AlecEdu_api.Domain.Entities;
using AlecEdu_api.Domain.Enums;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AlecEdu_api.Application.Features.V1.Users.Commands.Login;

public class LoginCommand: IRequest<BaseResponse<LoginResultDto>>
{
    public UserLoginDto Data { get; init; } = null!;
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, BaseResponse<LoginResultDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<LoginCommandHandler> _logger;
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenGenerator _generator;
    private readonly JwtSettings _jwtSettings;
    
    public LoginCommandHandler(
        ILogger<LoginCommandHandler> logger,
        IUnitOfWork unitOfWork,
        UserManager<User> userManager,
        IOptions<JwtSettings> jwSettings,
        IJwtTokenGenerator generator,
        IMapper mapper
    )
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
        _generator = generator;
        _jwtSettings = jwSettings.Value;
    }
    
    public async Task<BaseResponse<LoginResultDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("LoginCommandHandler@Handle -- Start");
        var response = new BaseResponse<LoginResultDto>(
            success: false
            );
        var validator = new LoginCommandValidator();
        var validationResult = await validator.ValidateAsync(request.Data, cancellationToken);
        
        if (validationResult.Errors.Count > 0)
        {
            _logger.LogInformation("LoginCommandHandler@Handle -- End -- Validate");
            throw new Exceptions.ValidationException(validationResult);
        }

        // var user = await _unitOfWork.GetRepository<User>()
        //     .FindByCondition(x => x.Name.Equals(request.Data.Name), true)
        //     .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        // var user = await ((_unitOfWork.GetRepository<User>() as IUserRepository)!).GetByNamAsync(request.Data.UserName, true);
        var user = await _userManager.FindByNameAsync(request.Data.UserName);
        if (await _userManager.CheckPasswordAsync(user, request.Data.Password))
        {
            var token = await _generator.GenerateToken(user);
            //save in db
            await _userManager.SetAuthenticationTokenAsync(
                user,
                _jwtSettings.RefreshTokenProviderName,
                _jwtSettings.RefreshTokenPurpose,
                token.RefreshToken
                );
            _unitOfWork.GetRepository<Token>().Add(new Token()
            {
                AccessToken = token.AccessToken,
                RefreshToken = token.RefreshToken,
                CreatedAt = DateTime.Now,
                ExpiresAt = token.Expires,
                UserId = user.Id
            });
            await _unitOfWork.SaveChangesAsync();
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