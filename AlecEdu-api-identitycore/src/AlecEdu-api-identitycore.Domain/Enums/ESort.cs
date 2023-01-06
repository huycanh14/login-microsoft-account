using System.Runtime.Serialization;

namespace AlecEdu_api.Domain.Enums;

public enum ESort
{
    [EnumMember(Value = "ASC")]
    ASC,
    [EnumMember(Value = "DESC")]
    DESC,
}
