using System.Reflection;
using AlecEdu_api.Domain.Common;
using AlecEdu_api.Domain.Entities;
using AlecEdu_api.Infrastructure.Persistence.Configurations;
using AlecEdu_api.Infrastructure.Persistence.Seeds;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Extensions;
using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AlecEdu_api.Infrastructure.Persistence;

public class AlecEduContext: IdentityDbContext<User, IdentityRole<int>, int>, IPersistedGrantDbContext, IConfigurationDbContext
{
    public AlecEduContext(){}
    public AlecEduContext(DbContextOptions<AlecEduContext> options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        base.OnConfiguring(builder);
    }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var options = new OperationalStoreOptions
        {
            DeviceFlowCodes = new TableConfiguration("DeviceFlowCodes"),
            PersistedGrants = new TableConfiguration("PersistedGrants")
        };

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
        modelBuilder.ConfigurePersistedGrantContext(options);
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            if (tableName != null && tableName.StartsWith("AspNet"))
            {
                tableName = tableName.Substring(6);
                entityType.SetTableName(tableName);
                if (tableName != null && tableName.EndsWith("s"))
                {
                    tableName = tableName.Substring(0, tableName.Length - 1);
                    entityType.SetTableName(tableName);
                }
            }
        }
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserEntityTypeConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccessTokenEntityTypeConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly(), 
            t => t.GetInterfaces().Any(i => 
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>) &&
                typeof(BaseEntity).IsAssignableFrom(i.GenericTypeArguments[0]))
        );
        modelBuilder.Entity<Tinh>().HasData(TinhSeed.getTinhs());
        modelBuilder.Entity<QuanHuyen>().HasData(QuanHuyenSeed.getQuanHuyen());
        modelBuilder.Entity<IdentityRole<int>>().HasData(RoleSeed.getRoles());
        modelBuilder.Entity<User>().HasData(UserSeed.getUsers());
        modelBuilder.Entity<IdentityUserRole<int>>().HasData(UserRolesSeed.getUserRoles());
        modelBuilder.Entity<IdentityRoleClaim<int>>().HasData(RoleClaimSeed.getRoleClaims());
        modelBuilder.EntityMappingsIdentityServer4();
    }
    
    public virtual DbSet<User> User { get; set; }
    public virtual DbSet<Tinh> Tinh { get; set; }
    public virtual DbSet<QuanHuyen> QuanHuyen { get; set; }
    public virtual DbSet<NhanVien> NhanVien { get; set; }
    public virtual DbSet<TienTe> TienTe { get; set; }
    public virtual DbSet<Token> Token { get; set; }
    public async Task<int> SaveChangesAsync() => await base.SaveChangesAsync();

    public virtual DbSet<PersistedGrant> PersistedGrants { get; set; }
    public virtual DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }
    public virtual DbSet<Client> Clients { get; set; }
    public virtual DbSet<ClientCorsOrigin> ClientCorsOrigins { get; set; }
    public virtual DbSet<IdentityResource> IdentityResources { get; set; }
    public virtual DbSet<ApiResource> ApiResources { get; set; }
    public virtual DbSet<ApiScope> ApiScopes { get; set; }
}
