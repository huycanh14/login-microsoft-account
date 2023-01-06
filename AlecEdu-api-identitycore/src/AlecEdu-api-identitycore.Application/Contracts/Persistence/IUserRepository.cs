using AlecEdu_api.Application.Common;
using AlecEdu_api.Domain.Entities;

namespace AlecEdu_api.Application.Contracts.Persistence;

public interface IUserRepository: IBaseRepository<User>
{
    Task<User> GetByNamAsync(string name, bool trackChanges = false);
}
