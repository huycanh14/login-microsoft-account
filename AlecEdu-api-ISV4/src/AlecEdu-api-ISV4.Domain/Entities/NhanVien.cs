using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AlecEdu_api.Domain.Common;

namespace AlecEdu_api.Domain.Entities;

public partial class NhanVien: BaseAuditableEntity
{
    [ForeignKey("User")]
    public int TaiKhoanId { get; set; }
    public virtual User User { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(19, 0)")]
    public decimal LuongCoBan { get; set; }
    
    [Required]
    [Column(TypeName = "varchar(30)")]
    public string SoTaiKhoan { get; set; }

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string NganHang { get; set; }

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string IdDinhDanh { get; set; }

    [Required]
    [Column(TypeName = "varchar(255)")]
    public string NoiCap { get; set; }

    [Required]
    [Column(TypeName = "varchar(255)")]
    public string QueQuan { get; set; }

    [ForeignKey("TienTe")]
    public int TienTeId { get; set; }
    public TienTe TienTe { get; set; }
}
