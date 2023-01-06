using AlecEdu_api.Application.Contracts.Infrastructure;
using AlecEdu_api.Application.Contracts.Persistence;
using AlecEdu_api.Infrastructure.Persistence;
using AlecEdu_api.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

namespace AlecEdu_api.Infrastructure.Services;

public class TokenManagerService : ITokenManagerService
{
    private readonly AlecEduContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenManagerService(
        AlecEduContext context,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<bool> IsCurrentActiveToken()
        => await IsActiveAsync(GetCurrentAsync());

    public async Task<bool> IsCurrentActiveTokenOrNull()
    {
        var token = GetCurrentAsync();
        if (token == "") return true;
        else return await IsActiveAsync(token);
    }

    public async Task DeactivateCurrentAsync()
        => await DeactivateAsync(GetCurrentAsync());

    public async Task<bool> IsActiveAsync(string token)
        => await _context.AccessToken
            .Where(x => x.AccessToken.Equals(token) && x.ExpiresAt > DateTime.Now)
            .FirstOrDefaultAsync() != null;

    public async Task DeactivateAsync(string token)
    {
        await _context.AccessToken
            .Where(x => x.AccessToken.Equals(token))
            .ExecuteDeleteAsync();
    }

    private string GetCurrentAsync()
    {
        if (_httpContextAccessor.HttpContext == null) return string.Empty;
        var authorizationHeader = _httpContextAccessor
            .HttpContext.Request.Headers["authorization"];

        return (authorizationHeader == StringValues.Empty
            ? string.Empty
            : authorizationHeader.Single()?.Split(" ").Last())!;

    }
}