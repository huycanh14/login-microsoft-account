using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AlecEdu_api.Infrastructure.Provider;

public class RefreshTokenProvider<TUser> : DataProtectorTokenProvider<TUser>
    where TUser : IdentityUser<int>
{
    public RefreshTokenProvider(IDataProtectionProvider dataProtectionProvider,
        IOptions<DataProtectionTokenProviderOptions> options, ILogger<DataProtectorTokenProvider<TUser>> logger) 
        : base(dataProtectionProvider, options, logger)
    {
        
    }
}
