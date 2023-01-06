using AlecEdu_api.Domain.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace AlecEdu_api.Infrastructure.Provider;

public class RefreshTokenProviderOptions: DataProtectionTokenProviderOptions
{
    private readonly JwtSettings _jwtSettings;
    public RefreshTokenProviderOptions(
        IOptions<JwtSettings> jwSettings)
    {
        _jwtSettings = jwSettings.Value;
        Name = _jwtSettings.RefreshTokenProviderName;
        TokenLifespan = TimeSpan.FromDays(_jwtSettings.RefreshExpireSeconds);
    }
}
