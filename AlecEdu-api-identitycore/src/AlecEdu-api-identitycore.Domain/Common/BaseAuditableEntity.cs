namespace AlecEdu_api.Domain.Common;

public abstract class BaseAuditableEntity: BaseTimeEntity
{
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public string? DeletedBy { get; set; }
}
