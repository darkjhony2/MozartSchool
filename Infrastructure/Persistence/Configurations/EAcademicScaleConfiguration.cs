using ColegioMozart.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColegioMozart.Infrastructure.Persistence.Configurations;

public class EAcademicScaleConfiguration : AuditableEntityMap<EAcademicScale, int>
{

    public override void Configure(EntityTypeBuilder<EAcademicScale> builder)
    {
        base.Configure(builder);

        builder.ToTable("academic_scales", options =>
        {
            options.IsTemporal();
        });


        builder.HasIndex(u => u.Name)
        .IsUnique();

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

    }

}
