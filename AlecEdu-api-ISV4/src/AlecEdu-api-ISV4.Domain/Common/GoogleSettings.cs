namespace AlecEdu_api.Domain.Common;

public class GoogleSettings
{
    public static string SectionName { get; } = "GoogleSettings";
    public string ClientIdWeb { get; set; }
    public string ClientIdiOS { get; set; }
    public string ClientIdAndroid { get; set; }
    public string ClientSecret { get; set; }
}
