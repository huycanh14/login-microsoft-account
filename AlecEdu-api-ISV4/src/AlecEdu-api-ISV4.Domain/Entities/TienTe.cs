using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AlecEdu_api.Domain.Common;

namespace AlecEdu_api.Domain.Entities;

public partial class TienTe: BaseTimeEntity
{
    public TienTe()
    {
        NhanViens = new HashSet<NhanVien>();
    }

    [Required]
    [Column(TypeName = "varchar(10)")]
    public string MaTienTe { get; set; }

    [Required]
    [Column(TypeName = "varchar(100)")]
    public string TenTienTe { get; set; }

    protected virtual ICollection<NhanVien> NhanViens { get; set; }
}
