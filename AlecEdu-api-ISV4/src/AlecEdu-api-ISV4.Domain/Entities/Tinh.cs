using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AlecEdu_api.Domain.Common;

namespace AlecEdu_api.Domain.Entities;

public partial class Tinh : BaseStatusEntity
{
    public Tinh()
    {
        Huyens = new HashSet<QuanHuyen>();
        Users = new HashSet<User>();
    }
    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string TenTinh { get; set; }

    protected virtual ICollection<QuanHuyen> Huyens { get; set; }
    protected virtual ICollection<User> Users { get; set; }
    
}
