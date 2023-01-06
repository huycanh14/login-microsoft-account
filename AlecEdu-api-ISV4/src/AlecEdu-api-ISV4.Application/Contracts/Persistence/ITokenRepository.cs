using AlecEdu_api.Application.Common;
using AlecEdu_api.Domain.Entities;

namespace AlecEdu_api.Application.Contracts.Persistence;

public interface  ITokenRepository: IBaseRepository<Token>
{
    Task RemoveTokenExpires();
}
