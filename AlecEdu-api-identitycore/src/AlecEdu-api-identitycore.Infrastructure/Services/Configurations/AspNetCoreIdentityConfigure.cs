using AlecEdu_api.Domain.Common;
using AlecEdu_api.Domain.Entities;
using AlecEdu_api.Infrastructure.Persistence;
using AlecEdu_api.Infrastructure.Provider;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AlecEdu_api.Infrastructure.Services.Configurations;

public static class AspNetCoreIdentityConfigure
{
    public static IServiceCollection AddAspNetCoreIdentityConfigure(
        this IServiceCollection services,
        JwtSettings jwtSetting
        )
    {
        services
            .AddIdentity<User, IdentityRole<int>>()
            .AddEntityFrameworkStores<AlecEduContext>()
            .AddDefaultTokenProviders()
            .AddTokenProvider<RefreshTokenProvider<User>>(jwtSetting.RefreshTokenProviderName)
            ;
        
       
        // Config IdentityOptions
        services.Configure<IdentityOptions>(options =>
        {
            // password
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 3;

            // user
            options.User.RequireUniqueEmail = true;
            
            // login
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;
            options.SignIn.RequireConfirmedAccount = true;
        });
        return services;
    }
}
