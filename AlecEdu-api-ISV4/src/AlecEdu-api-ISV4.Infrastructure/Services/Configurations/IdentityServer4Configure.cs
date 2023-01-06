using AlecEdu_api.Domain.Common;
using AlecEdu_api.Domain.Configurations;
using AlecEdu_api.Domain.Entities;
using AlecEdu_api.Infrastructure.Authentication;
using AlecEdu_api.Infrastructure.Persistence;
using AlecEdu_api.Infrastructure.Services.IdentityServer4;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AlecEdu_api.Infrastructure.Services.Configurations;

public static class IdentityServer4Configure
{
    public static IServiceCollection AddIdentityServer4Configure(
        this IServiceCollection services,
        JwtSettings jwtSetting, 
        DatabaseSettings databaseSettings
        )
    {
        var assembly = typeof(AlecEduContext).Assembly.FullName;
        services
            .AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.EmitStaticAudienceClaim = true;
            })
            // Use our SQL Database for storing configuration data 
            .AddConfigurationStore(configDb =>
            {
                configDb.ConfigureDbContext = db => db.UseSqlServer(databaseSettings.ConnectionString,
                    sql => sql.MigrationsAssembly(assembly));
            })
            .AddOperationalStore(operationalDb =>
            {
                operationalDb.ConfigureDbContext = db => db.UseSqlServer(databaseSettings.ConnectionString,
                    sql => sql.MigrationsAssembly(assembly));
                operationalDb.EnableTokenCleanup = true;
                operationalDb.TokenCleanupInterval = jwtSetting.AccessExpireSeconds;
            })
            .AddAspNetIdentity<User>()
            .AddJwtBearerClientAuthentication()
            .AddDeveloperSigningCredential()
            .AddProfileService<ProfileService>()
            .AddResourceOwnerValidator<OwnerPasswordValidator>()
            .AddExtensionGrantValidator<TokenExchangeGrantValidator>()
            ;
        return services;
    }
}
