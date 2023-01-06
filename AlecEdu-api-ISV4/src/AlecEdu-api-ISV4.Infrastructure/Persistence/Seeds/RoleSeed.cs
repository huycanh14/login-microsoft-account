using Microsoft.AspNetCore.Identity;

namespace AlecEdu_api.Infrastructure.Persistence.Seeds;

public static class RoleSeed
{
    public static IEnumerable<IdentityRole<int>> getRoles()
    {
        return new List<IdentityRole<int>>
        {
            new() { Id = 1, Name = "Admin", NormalizedName = "ADMIN"},
            new() { Id = 2, Name = "Staff", NormalizedName = "STAFF"},
        };
    }
}
