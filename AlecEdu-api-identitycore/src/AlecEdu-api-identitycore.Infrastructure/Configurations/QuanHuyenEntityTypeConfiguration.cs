using AlecEdu_api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlecEdu_api.Infrastructure.EntityTypeConfiguration;

public class QuanHuyenEntityTypeConfiguration: IEntityTypeConfiguration<QuanHuyen>
{
    public void Configure(EntityTypeBuilder<QuanHuyen> builder)
    {
        builder.HasOne(b => b.Tinh).WithMany().OnDelete(DeleteBehavior.ClientSetNull);
    }
}
