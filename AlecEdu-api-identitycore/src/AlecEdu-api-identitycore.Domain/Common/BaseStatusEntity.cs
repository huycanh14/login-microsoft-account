using System.ComponentModel;
using AlecEdu_api.Domain.Enums;

namespace AlecEdu_api.Domain.Common;

public abstract class BaseStatusEntity: BaseEntity
{
    [DefaultValue(EStatus.Active)]
    public EStatus TrangThai { get; set; } = EStatus.Active;
}