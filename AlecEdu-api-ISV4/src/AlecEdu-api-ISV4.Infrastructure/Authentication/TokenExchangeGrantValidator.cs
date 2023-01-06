using AlecEdu_api.Domain.Entities;
using IdentityModel;
using IdentityServer4.Events;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AlecEdu_api.Infrastructure.Authentication;

public class TokenExchangeGrantValidator : IExtensionGrantValidator
{
    private readonly UserManager<User> _userManager;
    private readonly IEventService _events;

    public TokenExchangeGrantValidator(
        ITokenValidator validator,
        IHttpContextAccessor httpContextAccessor,
        UserManager<User> userManager,
        IEventService events
    )
    {
        _userManager = userManager;
        _events = events;
    }

    public async Task ValidateAsync(ExtensionGrantValidationContext context)
    {
        var email = context.Request.Raw.Get("email");

        if (string.IsNullOrEmpty(email)) {
            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
            return;
        }

        var user = await _userManager.FindByEmailAsync(email);
        if (null == user) {
            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
            return;
        }

        await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id.ToString(), user.UserName, false, context.Request.ClientId));
        var customResponse = new Dictionary<string, object>
        {
            {OidcConstants.TokenResponse.IssuedTokenType, OidcConstants.TokenTypeIdentifiers.AccessToken}
        };
        context.Result = new GrantValidationResult(
            subject: user.Id.ToString(),
            authenticationMethod: GrantType,
            customResponse: customResponse);
    }

    public string GrantType => "login_external_sso";
}
