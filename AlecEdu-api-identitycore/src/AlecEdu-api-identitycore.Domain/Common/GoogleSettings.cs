namespace AlecEdu_api.Domain.Common;

public class GoogleSettings
{
    public static string SectionName { get; } = "GoogleSettings";
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    
}
