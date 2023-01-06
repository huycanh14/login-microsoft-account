using Hangfire.Dashboard;

namespace AlecEdu_api.Domain.Extensions;

public class AuthorizationFilter: IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context) => true;
}
