using AlecEdu_api.Domain.Common;
using AlecEdu_api.Domain.Configurations;
using AlecEdu_api.Infrastructure.Services.Configurations;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AlecEdu_api.Infrastructure.Persistence.Seeds;

public static class ConfigurationDbContextIdentityServer4Seed
{
    public static async Task IdentityServer4SeedAsync(AlecEduContext context, IConfiguration configuration)
        {

            //callbacks urls from config:
            var identityServer4Settings = new IdentityServer4Settings();
            var jwtSettings = new JwtSettings();
            configuration.GetSection(IdentityServer4Settings.SectionName).Bind(identityServer4Settings);
            configuration.GetSection(JwtSettings.SectionName).Bind(jwtSettings);
            var clientUrls = new Dictionary<string, string>
            {
                {"AlecEduWebAdmin", identityServer4Settings.AlecEduWebAdmin},
                {"AlecEduApiInternal", identityServer4Settings.AlecEduApiInternal},
                {"AlecEduWebApp", identityServer4Settings.AlecEduWebApp}
            };


            if (!context.Clients.Any())
            {
                foreach (var client in Config.GetClients(clientUrls, jwtSettings))
                {
                    context.Clients.Add(client.ToEntity());
                }
                await context.SaveChangesAsync();
            }
            else
            {
                List<ClientRedirectUri> oldRedirects = (await context.Clients.Include(c => c.RedirectUris).ToListAsync())
                    .SelectMany(c => c.RedirectUris)
                    .Where(ru => ru.RedirectUri.EndsWith("/o2c.html"))
                    .ToList();

                if (oldRedirects.Any())
                {
                    foreach (var ru in oldRedirects)
                    {
                        ru.RedirectUri = ru.RedirectUri.Replace("/o2c.html", "/oauth2-redirect.html");
                        context.Update(ru.Client);
                    }
                    await context.SaveChangesAsync();
                }
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in Config.GetIdentityResources())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                await context.SaveChangesAsync();
            }

            if (!context.ApiResources.Any())
            {
                foreach (var api in Config.GetApiResources())
                {
                    context.ApiResources.Add(api.ToEntity());
                }

                await context.SaveChangesAsync();
            }

            if (!context.ApiScopes.Any())
            {
                foreach (var resource in Config.GetApiScopes())
                {
                    context.ApiScopes.Add(resource.ToEntity());
                }
                await context.SaveChangesAsync();
            }
        }
}
