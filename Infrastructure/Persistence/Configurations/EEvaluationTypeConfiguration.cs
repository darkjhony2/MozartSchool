using ColegioMozart.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColegioMozart.Infrastructure.Persistence.Configurations;

public class EEvaluationTypeConfiguration : AuditableEntityMap<EEvaluationType, int>
{
    public override void Configure(EntityTypeBuilder<EEvaluationType> builder)
    {
        base.Configure(builder);

        builder.ToTable("evaluation_type", options =>
        {

        });

        builder.HasIndex(u => u.Name)
            .IsUnique();

        builder.Property(x => x.Name)
            .HasMaxLength(80);

        builder.Property(x => x.Description)
            .HasMaxLength(250);

    }


}
