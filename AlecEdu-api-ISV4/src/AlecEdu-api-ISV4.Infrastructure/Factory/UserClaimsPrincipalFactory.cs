using System.Security.Claims;
using AlecEdu_api.Domain.Entities;
using AlecEdu_api.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace AlecEdu_api.Infrastructure.Factory;

public class UserClaimsPrincipalFactory: UserClaimsPrincipalFactory<User, IdentityRole<int>>
{

    public UserClaimsPrincipalFactory(
        UserManager<User> userManager,
        RoleManager<IdentityRole<int>> roleManager, 
        IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
    {
        
    }
    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
    {
        var identity = await base.GenerateClaimsAsync(user);
        identity.AddClaim(new Claim("HoTen", user.HoTen ?? ""));
        return identity;
    }
}
