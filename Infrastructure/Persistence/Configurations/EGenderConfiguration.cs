using ColegioMozart.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColegioMozart.Infrastructure.Persistence.Configurations;

public class EGenderConfiguration : AuditableEntityMap<EGender, int>
{
    public override void Configure(EntityTypeBuilder<EGender> builder)
    {
        base.Configure(builder);

        builder.HasIndex(u => u.Name)
        .IsUnique();

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

    }

}
