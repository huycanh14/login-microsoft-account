using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace AlecEdu_api.Domain.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum EClientId
{
    [EnumMember(Value = "Web")]
    Web,
    [EnumMember(Value = "iOS")]
    iOS,
    [EnumMember(Value = "Android")]
    Android,
}
