using AlecEdu_api.Application.Middleware;
using Microsoft.AspNetCore.Builder;

namespace AlecEdu_api.Application;

public static class ConfigureBuilders
{
    public static IApplicationBuilder AddApplicationBuilders (this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}
