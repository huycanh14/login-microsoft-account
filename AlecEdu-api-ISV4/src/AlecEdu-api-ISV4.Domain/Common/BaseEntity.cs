using System.ComponentModel.DataAnnotations;

namespace AlecEdu_api.Domain.Common;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }
}
