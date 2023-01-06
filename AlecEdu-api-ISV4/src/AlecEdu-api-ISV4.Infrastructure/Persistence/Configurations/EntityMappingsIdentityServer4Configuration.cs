using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace AlecEdu_api.Infrastructure.Persistence.Configurations;

public static class EntityMappingsIdentityServer4Configuration
{
    public static ModelBuilder EntityMappingsIdentityServer4(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityResourceClaim>().ToTable("IdentityResourceClaims");
        modelBuilder.Entity<ApiResource>().ToTable("ApiResources");
        modelBuilder.Entity<ApiScope>().ToTable("ApiScopes");
        modelBuilder.Entity<Client>().ToTable("Clients");
        modelBuilder.Entity<IdentityResource>().ToTable("IdentityResources");
        modelBuilder.Entity<ApiResourceClaim>().ToTable("ApiResourceClaims");
        modelBuilder.Entity<ApiResourceProperty>().ToTable("ApiResourceProperties");
        modelBuilder.Entity<ApiResourceScope>().ToTable("ApiResourceScopes");
        modelBuilder.Entity<ApiResourceSecret>().ToTable("ApiResourceSecrets");
        modelBuilder.Entity<ApiScopeClaim>().ToTable("ApiScopeClaims");
        modelBuilder.Entity<ApiScopeProperty>().ToTable("ApiScopeProperties");
        modelBuilder.Entity<ClientClaim>().ToTable("ClientClaims");
        modelBuilder.Entity<ClientCorsOrigin>().ToTable("ClientCorsOrigins");
        modelBuilder.Entity<ClientGrantType>().ToTable("ClientGrantTypes");
        modelBuilder.Entity<ClientIdPRestriction>().ToTable("ClientIdPRestrictions");
        modelBuilder.Entity<ClientPostLogoutRedirectUri>().ToTable("ClientPostLogoutRedirectUris");
        modelBuilder.Entity<ClientProperty>().ToTable("ClientProperties");
        modelBuilder.Entity<ClientRedirectUri>().ToTable("ClientRedirectUris");
        modelBuilder.Entity<ClientScope>().ToTable("ClientScopes");
        modelBuilder.Entity<ClientSecret>().ToTable("ClientSecrets");
        modelBuilder.Entity<IdentityResourceClaim>().ToTable("IdentityResourceClaims");
        modelBuilder.Entity<IdentityResourceProperty>().ToTable("IdentityResourceProperties");
        return modelBuilder;
    }
}
