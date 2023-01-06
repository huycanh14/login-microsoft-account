using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AlecEdu_api.Domain.Common;

namespace AlecEdu_api.Domain.Entities;

public partial class QuanHuyen: BaseStatusEntity
{
    public QuanHuyen()
    {
        Users = new HashSet<User>();
    }

    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string TenQuanHuyen { get; set; } = default!;
    
    [ForeignKey("Tinh")]
    public int TinhId { get; set; }
    public Tinh Tinh { get; set; }

    protected virtual ICollection<User> Users { get; set; }
}
