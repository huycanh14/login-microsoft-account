using Microsoft.AspNetCore.Identity;

namespace AlecEdu_api.Infrastructure.Persistence.Seeds;

public static class UserRolesSeed
{
    public static IEnumerable<IdentityUserRole<int>> getUserRoles()
    {
        return new List<IdentityUserRole<int>>
        {
            new() { UserId = 1, RoleId = 1},
        };
    }
}
