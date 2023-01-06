using AlecEdu_api.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace AlecEdu_api.API.Common;

public class AuthorizeMultiplePolicyAttribute: TypeFilterAttribute
{
    private string Role { get; set; }
    private string? Policy { get; set; }
    public AuthorizeMultiplePolicyAttribute(string role = "", string policy = "") : base(typeof(AuthorizeMultiplePolicyFilter))
    {
        Role = role;
        Policy = policy;
        Arguments = new object[] { Role, Policy };
        // https://stackoverflow.com/questions/56563905/net-core-web-api-authorize-a-action-if-either-of-role-or-policy-matches
    }


}
