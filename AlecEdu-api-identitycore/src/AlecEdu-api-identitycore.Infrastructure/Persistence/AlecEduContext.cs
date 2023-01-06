
using System.Reflection;
using AlecEdu_api.Domain.Common;
using AlecEdu_api.Domain.Entities;
using AlecEdu_api.Infrastructure.EntityTypeConfiguration;
using AlecEdu_api.Infrastructure.Persistence.Seeds;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AlecEdu_api.Infrastructure.Persistence;

public class AlecEduContext: IdentityDbContext<User, IdentityRole<int>, int>{
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
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            if (tableName != null && tableName.StartsWith("AspNet"))
            {
                tableName = tableName.Substring(6);
                entityType.SetTableName(tableName);
            }

            if (tableName != null && tableName.EndsWith("s"))
            {
                tableName = tableName.Substring(0, tableName.Length - 1);
                entityType.SetTableName(tableName);
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
    }
    
    public DbSet<User> User { get; set; }
    public DbSet<Tinh> Tinh { get; set; }
    public DbSet<QuanHuyen> QuanHuyen { get; set; }
    public DbSet<NhanVien> NhanVien { get; set; }
    public DbSet<TienTe> TienTe { get; set; }
    public DbSet<Token> AccessToken { get; set; }
}
