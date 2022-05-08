using ColegioMozart.Domain.Entities;
using ColegioMozart.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColegioMozart.Infrastructure.Persistence.Configurations;

public class EAcademicPeriodConfiguration : AuditableEntityMap<EAcademicPeriod, Guid>
{

    public override void Configure(EntityTypeBuilder<EAcademicPeriod> builder)
    {
        base.Configure(builder);

        builder.ToTable("academic_periods", options =>
        {
            options.IsTemporal();
        });

        builder.Property(x => x.StartDate)
            .HasConversion<DateOnlyConverter, DateOnlyComparer>();

        builder.Property(x => x.EndDate)
            .HasConversion<DateOnlyConverter, DateOnlyComparer>();

        builder.HasIndex(u => new { u.Year , u.Name})
        .IsUnique();

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

    }

}
