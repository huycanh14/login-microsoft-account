using AlecEdu_api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlecEdu_api.Infrastructure.Persistence.Configurations;

public class AccessTokenEntityTypeConfiguration
{
    public void Configure(EntityTypeBuilder<Token> builder)
    {
        builder.Property(b => b.CreatedAt).HasDefaultValueSql("(getutcdate())");
        builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
        builder.HasIndex(e => e.AccessToken)
            .HasName("IX_AccessToken_Token");
        builder.Property(x => x.AccessToken).HasColumnType("nvarchar(MAX)");
        builder.Property(x => x.RefreshToken).HasColumnType("nvarchar(MAX)");
        builder.HasIndex(e => e.UserId)
            .HasName("IX_AccessToken_User");
    }
}
