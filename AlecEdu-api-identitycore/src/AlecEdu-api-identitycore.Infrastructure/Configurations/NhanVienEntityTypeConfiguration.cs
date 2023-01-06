using AlecEdu_api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlecEdu_api.Infrastructure.EntityTypeConfiguration;

public class NhanVienEntityTypeConfiguration: BaseTimeEntityTypeConfiguration<NhanVien>
{
    public void Configure(EntityTypeBuilder<NhanVien> builder)
    {
        base.Configure(builder);
        builder.HasOne(b => b.TienTe).WithMany().OnDelete(DeleteBehavior.Restrict);
    }
}
