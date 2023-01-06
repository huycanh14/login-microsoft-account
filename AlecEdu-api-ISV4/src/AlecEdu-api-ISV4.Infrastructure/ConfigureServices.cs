using AlecEdu_api.Application.Common;
using AlecEdu_api.Application.Common.Interfaces.Authentication;
using AlecEdu_api.Application.Common.Interfaces.Services;
using AlecEdu_api.Application.Contracts.Infrastructure;
using AlecEdu_api.Application.Contracts.ScheduledJobs;
using AlecEdu_api.Domain.Common;
using AlecEdu_api.Domain.Configurations;
using AlecEdu_api.Domain.Entities;
using AlecEdu_api.Domain.Extensions;
using AlecEdu_api.Infrastructure.Authentication;
using AlecEdu_api.Infrastructure.Common;
using AlecEdu_api.Infrastructure.Factory;
using AlecEdu_api.Infrastructure.Persistence;
using AlecEdu_api.Infrastructure.ScheduledJobs;
using AlecEdu_api.Infrastructure.Services;
using AlecEdu_api.Infrastructure.Services.Configurations;
using AlecEdu_api.Infrastructure.Services.IdentityServer4;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AlecEdu_api.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        var databaseSettings = services.GetOptions<DatabaseSettings>(nameof(DatabaseSettings));
        if (databaseSettings == null || string.IsNullOrEmpty(databaseSettings.ConnectionString))
            throw new ArgumentNullException("Connection string is not configured.");
        
        services.AddDbContext<AlecEduContext>(options =>
        {
            options.UseSqlServer(databaseSettings.ConnectionString,
                builder =>
                    builder.MigrationsAssembly(typeof(AlecEduContext).Assembly.FullName));
        });
        
        var jwtSetting = new JwtSettings();
        var googleSetting = new GoogleSettings();
        services.Configure<JwtSettings>(c => configuration.GetSection(JwtSettings.SectionName).Bind(c));
        configuration.GetSection(JwtSettings.SectionName).Bind(jwtSetting);
        
        services.Configure<GoogleSettings>(c => configuration.GetSection(GoogleSettings.SectionName).Bind(c));
        configuration.GetSection(GoogleSettings.SectionName).Bind(googleSetting);
        
        var identityServer4Settings = new IdentityServer4Settings();
        services.Configure<IdentityServer4Settings>(c => configuration.GetSection(IdentityServer4Settings.SectionName).Bind(c));
        configuration.GetSection(IdentityServer4Settings.SectionName).Bind(identityServer4Settings);
        
        services
            .AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services
            .AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>))
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IJwtTokenGenerator, JwtTokenGenerator>()
            .AddScoped<IRecurringJob, HangfireRecurringJob>()
            .AddScoped<IUserClaimsPrincipalFactory<User>, UserClaimsPrincipalFactory>()
            ;
        services.AddDIRepositoryConfigure();
        services
            .AddTransient<IScheduledJobService, HangfireService>()
            .AddTransient<ITokenManagerService, TokenManagerService>()
            .AddTransient<IProfileService, ProfileService>()
            .AddTransient<IResourceOwnerPasswordValidator, OwnerPasswordValidator>()
            ;
        
        // Dang ky Identity
        services.AddAspNetCoreIdentityConfigure(jwtSetting);
        
        // dang ky identity server
        services.AddIdentityServer4Configure(jwtSetting, databaseSettings);

        // config jwt authentication
        services.AddAuthConfigure(jwtSetting, identityServer4Settings);
        
        // config hangfire
        services.AddHangfireConfigure(configuration);
        
        return services;
    }
}
