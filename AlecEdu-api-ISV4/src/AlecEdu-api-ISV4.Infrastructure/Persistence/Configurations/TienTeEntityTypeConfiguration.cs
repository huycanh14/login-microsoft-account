using AlecEdu_api.Domain.Entities;
using AlecEdu_api.Domain.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlecEdu_api.Infrastructure.Persistence.Configurations;

public class TienTeEntityTypeConfiguration: BaseTimeEntityTypeConfiguration<TienTe>
{
    public void Configure(EntityTypeBuilder<TienTe> builder)
    {
        base.Configure(builder);
    }
}
