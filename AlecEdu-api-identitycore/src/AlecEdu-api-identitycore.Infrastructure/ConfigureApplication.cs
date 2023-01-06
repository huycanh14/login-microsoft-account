using AlecEdu_api.Application.Contracts.ScheduledJobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AlecEdu_api.Infrastructure;

public static class ConfigureApplication
{
    public static IApplicationBuilder AddInfrastructureApplication(this IApplicationBuilder app, WebApplicationBuilder builder)
    {
        var provider = builder.Services.BuildServiceProvider();
        var recurringJob = provider.GetRequiredService<IRecurringJob>();
        recurringJob.Run();

        return app;
    }
}
