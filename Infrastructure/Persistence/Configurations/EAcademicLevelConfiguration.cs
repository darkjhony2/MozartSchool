using ColegioMozart.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColegioMozart.Infrastructure.Persistence.Configurations;

public class EAcademicLevelConfiguration : AuditableEntityMap<EAcademicLevel, int>
{

    public override void Configure(EntityTypeBuilder<EAcademicLevel> builder)
    {
        base.Configure(builder);

        builder.ToTable("academic_levels", options =>
        {
            
            options.IsTemporal();
        });


        builder.HasIndex(u => u.Level)
        .IsUnique();

        builder.Property(x => x.Level)
            .HasMaxLength(100)
            .IsRequired();

    }
}
