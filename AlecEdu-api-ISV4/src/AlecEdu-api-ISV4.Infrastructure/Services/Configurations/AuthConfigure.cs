using System.Security.Claims;
using System.Text;
using AlecEdu_api.Domain.Common;
using AlecEdu_api.Domain.Configurations;
using AlecEdu_api.Domain.Enums;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace AlecEdu_api.Infrastructure.Services.Configurations;

public static class AuthConfigure
{
    public static IServiceCollection AddAuthConfigure(
        this IServiceCollection services, 
        JwtSettings jwtSetting,
        IdentityServer4Settings identityServer4Settings)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddIdentityServerAuthentication(options =>
            {
                options.Authority = identityServer4Settings.AlecEduApiInternal;
                options.ApiName = "full_access";
                options.ApiSecret = "secret";
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.EnableCaching = true;
            })
            // .AddJwtBearer(options =>
            // {
            //     options.RequireHttpsMetadata = false;
            //     options.SaveToken = true;
            //     options.TokenValidationParameters = new TokenValidationParameters
            //     {
            //         ValidateIssuerSigningKey = true,
            //         ValidateIssuer = true,
            //         ValidateAudience = true,
            //         ValidAudience = jwtSetting.Audience,
            //         ValidIssuer = jwtSetting.Issuer,
            //         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Secret)),
            //         ClockSkew = TimeSpan.Zero
            //     };
            // })
            ;
        services.AddAuthorization(options =>
        {
            var modules = typeof(BaseModule)
            .Assembly.GetTypes()
            .Where(t => t.IsSubclassOf(typeof(BaseModule)) && !t.IsAbstract)
            .Select(t => (BaseModule)Activator.CreateInstance(t)!);
            foreach (var module in modules)
            {
                foreach(KeyValuePair<EActionModule, string> entry in module.ClaimValues)
                {
                    // do something with entry.Value or entry.Key
                    options.AddPolicy($"{module.PolicyName}{entry.Key}", policy => 
                        // policy.RequireClaim(module.ClaimType, entry.Value)
                        policy.RequireAssertion(context =>
                            context.User.HasClaim(ClaimTypes.Role, nameof(ETypeUser.Develop)) ||
                            context.User.HasClaim(ClaimTypes.Role, nameof(ETypeUser.Admin)) ||
                            context.User.HasClaim(module.ClaimType, entry.Value))
                        );
                }
            }
        });
        return services;
    }
}
