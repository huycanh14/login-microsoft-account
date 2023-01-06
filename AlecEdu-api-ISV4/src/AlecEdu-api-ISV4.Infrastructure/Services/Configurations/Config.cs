using AlecEdu_api.Domain.Common;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace AlecEdu_api.Infrastructure.Services.Configurations;

/***
 * Resources: là các API cần bảo vệ bởi IdentityServer4.
 *            Có 2 loại: Identity resource và Api resource
 */
public static class Config
{
    // config Identity resource: Đại diện cho các yêu cầu về user như id, name, email...
    public static IEnumerable<IdentityResource> GetIdentityResources()
    {
        return new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };
    }
    
    // config Identity resource: Đại diện cho chức năng mà user muốn truy cập. Thông thường, chúng là các endpoints HTTP (còn gọi là API), nhưng cũng có thể là các message queuing endpoints hoặc tương tự
    public static IEnumerable<ApiResource> GetApiResources()
    {
        return new[]
        {
            // Khai báo các service api
           new ApiResource
           {
               Name = "full_access",
               DisplayName = "AlecEdu Api",
               UserClaims =
               {
                   JwtClaimTypes.Email,
                   JwtClaimTypes.Audience,
                   JwtClaimTypes.Issuer,
                   JwtClaimTypes.JwtId
               },Scopes = new List<string>
               {
                   "full_access"
               },
           }
        };
    }
    
    // scope: Khai báo các thao tác của hệ thống
    public static IEnumerable<ApiScope> GetApiScopes()
    {
        return new[]
        {
            new ApiScope()
            {
                DisplayName = "full_access",
                Enabled = true,
                Name = "full_access",
                ShowInDiscoveryDocument = true,
            }
        };
    }

    // client: là một phần mềm ứng dụng kiểu như trình duyệt web, mobile app hay bất cứ cái gì đang cần gọi một API resource.
    public static IEnumerable<Client> GetClients(Dictionary<string, string> clientUrls, JwtSettings jwtSettings)
    {
        var _grantTypesApiInternal = GrantTypes.ResourceOwnerPasswordAndClientCredentials.ToList();
        _grantTypesApiInternal.Add("login_external_sso");
        return new List<Client>()
        {
            new Client
            {
                ClientId = "alec_edu_web_app",
                ClientName = "AlecEdu WebApp Client",
                ClientSecrets = new List<Secret>
                {
                    new Secret("secret".Sha256())
                },
                ClientUri = $"{ clientUrls["AlecEduWebApp"] }", // public uri of the client
                AllowedCorsOrigins = { clientUrls["AlecEduWebApp"] },
                AllowedGrantTypes = GrantTypes.Code,
                AllowAccessTokensViaBrowser = false,
                RequireConsent = false,
                AllowOfflineAccess = false,
                AlwaysIncludeUserClaimsInIdToken = true,
                RedirectUris = new List<string>
                {
                    $"{clientUrls["AlecEduWebApp"]}/authentication/login"
                },
                PostLogoutRedirectUris = new List<string>
                {
                    $"{clientUrls["AlecEduWebApp"]}/authentication/logout"
                },
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    "full_access"
                },
                AccessTokenLifetime = jwtSettings.AccessExpireSeconds,
                IdentityTokenLifetime = jwtSettings.AccessExpireSeconds,
                RequireClientSecret = true // Important for authorization
            },
            new Client
            {
                ClientId = "alec_edu_web_admin",
                ClientName = "AlecEdu WebApp Admin",
                ClientSecrets = new List<Secret>
                {
                    new Secret("secret".Sha256())
                },
                ClientUri = $"{ clientUrls["AlecEduWebAdmin"] }", // public uri of the client
                AllowedCorsOrigins = { clientUrls["AlecEduWebAdmin"] },
                AllowedGrantTypes = GrantTypes.Code,
                AllowAccessTokensViaBrowser = false,
                RequireConsent = false,
                AllowOfflineAccess = false,
                AlwaysIncludeUserClaimsInIdToken = true,
                RedirectUris = new List<string>
                {
                    $"{clientUrls["AlecEduWebAdmin"]}/authentication/login"
                },
                PostLogoutRedirectUris = new List<string>
                {
                    $"{clientUrls["AlecEduWebAdmin"]}/authentication/logout"
                },
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    "full_access"
                },
                AccessTokenLifetime = jwtSettings.AccessExpireSeconds,
                IdentityTokenLifetime = jwtSettings.AccessExpireSeconds,
                RequireClientSecret = true // Important for authorization
            },
            new Client
            {
                ClientId = "alec_edu_api_internal",
                ClientName = "AlecEdu Api Internal",
                ClientSecrets = new List<Secret>
                {
                    new Secret("secret".Sha256())
                },
                ClientUri = $"{ clientUrls["AlecEduApiInternal"] }", // public uri of the client
                AllowedCorsOrigins = { clientUrls["AlecEduApiInternal"] },
                AllowedGrantTypes = _grantTypesApiInternal, //.conGrantTypes.ResourceOwnerPasswordAndClientCredentials.ToArray(),
                AllowAccessTokensViaBrowser = false,
                RequireConsent = false,
                AllowOfflineAccess = true,
                AlwaysIncludeUserClaimsInIdToken = true,
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    "full_access"
                },
                AccessTokenLifetime = jwtSettings.AccessExpireSeconds,
                IdentityTokenLifetime = jwtSettings.AccessExpireSeconds,
                RequireClientSecret = true, // Important for authorization
                RefreshTokenUsage = TokenUsage.OneTimeOnly,
                RefreshTokenExpiration = TokenExpiration.Sliding,
                AbsoluteRefreshTokenLifetime = jwtSettings.RefreshExpireSeconds,
                UpdateAccessTokenClaimsOnRefresh = true,
            },
        };
    }
}
