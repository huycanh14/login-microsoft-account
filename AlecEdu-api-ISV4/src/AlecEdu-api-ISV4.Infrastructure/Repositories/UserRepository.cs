using AlecEdu_api.Application.Contracts.Persistence;
using AlecEdu_api.Domain.Entities;
using AlecEdu_api.Infrastructure.Common;
using AlecEdu_api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace AlecEdu_api.Infrastructure.Repositories;

public class UserRepository:BaseRepository<User>, IUserRepository
{
    private readonly AlecEduContext _context;
    public UserRepository(AlecEduContext context) : base(context)
    {
        _context = context;
    }

    public async Task<User> GetByNamAsync(string name, bool trackChanges = false)
    {
        var user = await _context.User.FirstOrDefaultAsync(x => x.UserName.Equals(name));
        if (user != null && !trackChanges)
        {
            _context.Entry(user).State = EntityState.Detached;
        }

        return user;
    }
}
