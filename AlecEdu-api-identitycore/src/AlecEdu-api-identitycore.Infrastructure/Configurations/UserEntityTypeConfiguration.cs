using AlecEdu_api.Domain.Entities;
using AlecEdu_api.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlecEdu_api.Infrastructure.EntityTypeConfiguration;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasOne(b => b.Tinh).WithMany().OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(b => b.QuanHuyen).WithMany().OnDelete(DeleteBehavior.Restrict);
        builder.Property(b => b.CreatedAt).HasDefaultValueSql("(getdate())");
        builder.Property(b => b.GioTinh).HasDefaultValue(EGioiTinh.Male);
        builder.Property(b => b.TrangThai).HasDefaultValue(EUserStatus.Active);
        builder.Property(b => b.LoaiTaiKhoan).HasDefaultValue(ETypeUser.NhanVienTuVan);
    }
}
