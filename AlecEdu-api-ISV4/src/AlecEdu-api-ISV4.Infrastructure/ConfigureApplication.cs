using AlecEdu_api.Application.Contracts.ScheduledJobs;
using AlecEdu_api.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
