using ColegioMozart.Domain.Entities;
using ColegioMozart.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColegioMozart.Infrastructure.Persistence.Configurations;

public class EAttendanceRecordConfiguration : AuditableEntityMap<EAttendanceRecord, Guid>
{

    public override void Configure(EntityTypeBuilder<EAttendanceRecord> builder)
    {
        base.Configure(builder);

        builder.ToTable("attendace_records", options =>
        {
            options.IsTemporal();
        });


        builder.HasIndex(u => new { u.StudentId, u.Date })
        .IsUnique();

        builder.Property(x => x.Comments)
            .HasMaxLength(150);

        builder.Property(x => x.Date)
          .HasConversion<DateOnlyConverter, DateOnlyComparer>();

        builder.Property(x => x.StudentId).IsRequired();
        builder.Property(x => x.AttendanceStatusId).IsRequired();
        builder.Property(x => x.AcademicPeriodId).IsRequired();

        builder.HasOne(x => x.AttendanceStatus)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.AcademicPeriod)
           .WithMany()
           .OnDelete(DeleteBehavior.Restrict);

    }
}
