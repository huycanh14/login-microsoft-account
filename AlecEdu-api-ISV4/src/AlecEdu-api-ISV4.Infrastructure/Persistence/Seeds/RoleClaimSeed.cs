using AlecEdu_api.Domain.Modules;
using Microsoft.AspNetCore.Identity;

namespace AlecEdu_api.Infrastructure.Persistence.Seeds;

public static class RoleClaimSeed
{
    public static IEnumerable<IdentityRoleClaim<int>> getRoleClaims()
    {
        return new List<IdentityRoleClaim<int>>
        {
            new() { Id = 1, RoleId = 1, ClaimType = "users", ClaimValue = "users.list"},
            new() { Id = 2, RoleId = 1, ClaimType = "users", ClaimValue = "users.create"},
            new() { Id = 3, RoleId = 1, ClaimType = "users", ClaimValue = "users.update"},
            new() { Id = 4, RoleId = 1, ClaimType = "users", ClaimValue = "users.delete"},
        };
    }
}