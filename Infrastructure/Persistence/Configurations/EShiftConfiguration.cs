using ColegioMozart.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColegioMozart.Infrastructure.Persistence.Configurations;

public class EShiftConfiguration : AuditableEntityMap<EShift, int>
{

    public override void Configure(EntityTypeBuilder<EShift> builder)
    {
        base.Configure(builder);

        builder.ToTable("shifts", options =>
        {
        });

        builder.HasIndex(u => u.Name)
       .IsUnique();

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

    }

}
