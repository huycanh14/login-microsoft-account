using AlecEdu_api.Application.Contracts.Persistence;
using AlecEdu_api.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AlecEdu_api.Infrastructure.Services.Configurations;

public static class DIRepositoryConfigure
{
    public static IServiceCollection AddDIRepositoryConfigure(this IServiceCollection services)
    {
        services.AddScoped<ITokenRepository, TokenRepository>();
        return services;
    }
}
