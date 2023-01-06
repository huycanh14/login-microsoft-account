namespace AlecEdu_api.Domain.Common;

public class JwtSettings
{
    public static string SectionName { get; } = "JwtSettings";
    public string Secret { get; set; } = null!;
    public int AccessExpireSeconds { get; set; } = 0;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public string RefreshTokenProviderName { get; set; } = null!;
    public string RefreshTokenPurpose { get; set; } = null!;
    public int RefreshExpireSeconds { get; set; } = 0;
}
