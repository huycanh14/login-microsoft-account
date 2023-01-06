namespace AlecEdu_api.Domain.Configurations;

public class IdentityServer4Settings
{
    public static string SectionName { get; } = "IdentityServer4Settings";
    public string AlecEduWebApp { get; set; }
    public string AlecEduWebAdmin { get; set; }
    public string AlecEduApiInternal { get; set; }
}
