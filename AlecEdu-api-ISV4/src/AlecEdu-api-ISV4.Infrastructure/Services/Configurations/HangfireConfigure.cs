using AlecEdu_api.Domain.Configurations;
using Hangfire;
using Hangfire.Console.Extensions;
using Hangfire.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AlecEdu_api.Infrastructure.Services.Configurations;

public static class HangfireConfigure
{
    public static IServiceCollection AddHangfireConfigure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var hangFireSettings = configuration.GetSection(nameof(HangFireSettings))
            .Get<HangFireSettings>();
        services.AddSingleton(hangFireSettings);
        services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(hangFireSettings.Storage.ConnectionString, new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true
            }));

        // Add the processing server as IHostedService
        services.AddHangfireServer(serviceOptions => { serviceOptions.ServerName = hangFireSettings.ServerName; });
        services.AddHangfireConsoleExtensions();
        return services;
    }
}
