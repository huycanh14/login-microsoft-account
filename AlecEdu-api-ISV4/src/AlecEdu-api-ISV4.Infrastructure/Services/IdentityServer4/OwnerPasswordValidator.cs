using System.Security.Claims;
using AlecEdu_api.Domain.Entities;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;

namespace AlecEdu_api.Infrastructure.Services.IdentityServer4;

public class OwnerPasswordValidator: IResourceOwnerPasswordValidator
{
    private readonly UserManager<User> _userManager;
    public OwnerPasswordValidator(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        var user = await _userManager.FindByNameAsync(context.UserName);

        if (user == null)
        {
            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Username or password is incorrect");
            return;
        }

        var passwordValid = await _userManager.CheckPasswordAsync(user, context.Password);
        if (!passwordValid)
        {
            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Username or password is incorrect");
            return;
        }
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email)
        };
        context.Result = new GrantValidationResult(user.Id.ToString(), "password", claims);
    }
}
