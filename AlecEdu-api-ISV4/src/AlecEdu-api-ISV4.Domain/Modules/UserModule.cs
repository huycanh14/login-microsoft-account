using AlecEdu_api.Domain.Common;
using AlecEdu_api.Domain.Enums;

namespace AlecEdu_api.Domain.Modules;

public sealed class UserModule: BaseModule
{
    public const string Name = "UserPolicy";
    public override string ClaimType => "users";
    public override string PolicyName => Name;
    public override Dictionary<EActionModule, string> ClaimValues =>
        new()
        {
            { EActionModule.List, $"{ClaimType}.list" },
            { EActionModule.Create, $"{ClaimType}.create" },
            { EActionModule.Update, $"{ClaimType}.update" },
            { EActionModule.Delete, $"{ClaimType}.delete" },
        };
}

