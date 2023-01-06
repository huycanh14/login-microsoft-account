using AlecEdu_api.Domain.Configurations;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace AlecEdu_api.Domain.Extensions;

public static class HostExtensions
{
    public static void AddAppConfigurations(this ConfigureHostBuilder host)
    {
        host.ConfigureAppConfiguration((context, config) =>
        {
            var env = context.HostingEnvironment;
            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
        });
    }

    public static IApplicationBuilder UseHangfireDashboardDomain(this IApplicationBuilder app, IConfiguration configuration)
    {
        var hangFireSettings = configuration.GetSection(nameof(HangFireSettings))
            .Get<HangFireSettings>();
        var configDashboard = hangFireSettings.Dashboard;
        var hangfireRoute = hangFireSettings.Route;

        app.UseHangfireDashboard(hangfireRoute, new DashboardOptions
        {
            Authorization = new [] { new AuthorizationFilter() },
            DashboardTitle = configDashboard.DashboardTitle,
            StatsPollingInterval = configDashboard.StatsPollingInterval,
            AppPath = configDashboard.AppPath,
            IgnoreAntiforgeryToken = true
        });

        return app;
    }
}
