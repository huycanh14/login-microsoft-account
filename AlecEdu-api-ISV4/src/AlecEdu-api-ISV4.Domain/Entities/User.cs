using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AlecEdu_api.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace AlecEdu_api.Domain.Entities;

public partial class User : IdentityUser<int>
{
    public string? PasswordOrigin { get; set; }

    [Required] 
    [Column(TypeName = "nvarchar(100)")]
    public string HoTen { get; set; } = default!;
    [Required] 
    [Column(TypeName = "date")] 
    public DateTime NgaySinh { get; set; }
    public EGioiTinh GioTinh { get; set; } = EGioiTinh.Male;
    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string DiaChi { get; set; } = default!;
    [ForeignKey("Tinh")]
    public int TinhId { get; set; }
    public virtual Tinh Tinh { get; set; }
    [ForeignKey("QuanHuyen")]
    public int QuanHuyenId { get; set; }
    public virtual QuanHuyen QuanHuyen { get; set; }
    public EUserStatus TrangThai { get; set; } = EUserStatus.Active;
    public ETypeUser LoaiTaiKhoan { get; set; } = ETypeUser.NhanVienTuVan;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    
}
