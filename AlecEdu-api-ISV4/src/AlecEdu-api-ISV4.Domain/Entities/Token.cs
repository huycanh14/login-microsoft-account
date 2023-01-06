using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlecEdu_api.Domain.Entities;

public partial class Token
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public Guid Id { get; set; }
    public int UserId {get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime  AccessTokenExpiresAt { get; set; }
    public DateTime  RefreshTokenExpiresAt { get; set; }
}
