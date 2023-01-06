using AlecEdu_api.Domain.Enums;

namespace AlecEdu_api.Domain.Common;

public abstract class BaseModule
{
    public abstract string ClaimType { get; }
    public abstract string PolicyName { get; }
    public abstract Dictionary<EActionModule, string> ClaimValues { get; }
}
