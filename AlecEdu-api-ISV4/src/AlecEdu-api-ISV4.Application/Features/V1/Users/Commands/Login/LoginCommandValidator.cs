using AlecEdu_api.Application.Models;
using FluentValidation;

namespace AlecEdu_api.Application.Features.V1.Users.Commands.Login;

public class LoginCommandValidator: AbstractValidator<UserLoginDto>
{
    public LoginCommandValidator()
    {
        RuleFor(p => p.UserName)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();


        RuleFor(p => p.Password)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();

    }
}

public class LoginWithGoogleCommandValidator: AbstractValidator<UserLoginExternalDto>
{
    public LoginWithGoogleCommandValidator()
    {
        RuleFor(p => p.Token)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();

    }
}
