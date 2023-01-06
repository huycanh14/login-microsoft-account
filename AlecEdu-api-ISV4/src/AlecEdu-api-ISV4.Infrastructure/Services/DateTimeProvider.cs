using AlecEdu_api.Application.Common.Interfaces.Services;

namespace AlecEdu_api.Infrastructure.Services;

public class DateTimeProvider: IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
    public DateTime Now => DateTime.Now;
}
