using AlecEdu_api.Domain.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AlecEdu_api.Application.Models;

public class UserLoginExternalDto
{
    public string Token { get; set; }
    public EClientId? Client { get; set; }
}
