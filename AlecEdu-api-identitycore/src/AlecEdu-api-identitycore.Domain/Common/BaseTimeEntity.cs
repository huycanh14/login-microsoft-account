namespace AlecEdu_api.Domain.Common;

public class BaseTimeEntity: BaseStatusEntity
{
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}
