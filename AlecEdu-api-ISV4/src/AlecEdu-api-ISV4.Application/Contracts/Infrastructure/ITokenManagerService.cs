namespace AlecEdu_api.Application.Contracts.Infrastructure;

public interface ITokenManagerService
{
    Task<bool> IsCurrentActiveToken();
    Task<bool> IsCurrentActiveTokenOrNull();
    Task DeactivateCurrentAsync();
    Task<bool> IsActiveAsync(string token);
    Task DeactivateAsync(string token);

}
