using AlecEdu_api.Domain.Common;
using AlecEdu_api.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlecEdu_api.Infrastructure.EntityTypeConfiguration;

public class BaseTimeEntityTypeConfiguration<T>: IEntityTypeConfiguration<T>
where T: BaseTimeEntity
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(b => b.CreatedAt).HasDefaultValueSql("(getdate())");
        builder.Property(b => b.TrangThai).HasDefaultValue(EStatus.Active);
    }
}
