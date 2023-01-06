using AlecEdu_api.Application.Contracts.Persistence;
using AlecEdu_api.Domain.Entities;
using AlecEdu_api.Infrastructure.Common;
using AlecEdu_api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AlecEdu_api.Infrastructure.Repositories;

public class AccessTokenRepository : BaseRepository<Token>, IAccessTokenRepository
{
    private readonly AlecEduContext _context;
    public AccessTokenRepository(AlecEduContext context) : base(context)
    {
        _context = context;
    }

    public async Task RemoveTokenExpires()
    {
        var tokens = await _context.AccessToken.Where(x => x.ExpiresAt <= DateTime.Now).ToListAsync();
        _context.RemoveRange(tokens);
        await _context.SaveChangesAsync();
    }
}
