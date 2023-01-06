using System.Security.Claims;
using AlecEdu_api.Domain.Entities;
using AlecEdu_api.Domain.Enums;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;

namespace AlecEdu_api.Infrastructure.Services.IdentityServer4;

public class ProfileService: IProfileService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;

    public ProfileService(
        UserManager<User> userManager,
        RoleManager<IdentityRole<int>> roleManager
        )
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    
    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var subject = context.Subject ?? throw new ArgumentNullException(nameof(context.Subject));

        var subjectId = subject.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
        var user = await _userManager.FindByIdAsync(subjectId);
        var claims = await _userManager.GetClaimsAsync(user);
        var userRoles = await _userManager.GetRolesAsync(user);
        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        switch (user.LoaiTaiKhoan)
        {
            case ETypeUser.SuperAdmin:
                authClaims.Add(new Claim(ClaimTypes.Role, nameof(ETypeUser.SuperAdmin)));
                break;
            case ETypeUser.Develop:
                authClaims.Add(new Claim(ClaimTypes.Role, nameof(ETypeUser.Develop)));
                break;
            default:
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    var role = await _roleManager.FindByNameAsync(userRole);

                    if (role == null) continue;
                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    authClaims.AddRange(roleClaims);
                }
                break;
        }
        context.IssuedClaims.AddRange(claims);
        context.IssuedClaims.AddRange(authClaims);
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var user = await _userManager.GetUserAsync(context.Subject);
        context.IsActive = user is { DeletedAt: null } && user.TrangThai != EUserStatus.Locked;
    }
}
