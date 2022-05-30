using ColegioMozart.Domain.Entities;
using ColegioMozart.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColegioMozart.Infrastructure.Persistence.Configurations;

public class EClassScheduleConfiguration : AuditableEntityMap<EClassSchedule, Guid>
{

    public override void Configure(EntityTypeBuilder<EClassSchedule> builder)
    {
        base.Configure(builder);

        builder.ToTable("class_schedule", options =>
        {

        });


        builder.HasIndex(u => new { u.SubjectId, u.ClassRoomId, u.DayOfWeek, u.StartTime, u.EndTime })
            .IsUnique();

        builder.HasOne(x => x.Subject)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ClassRoom)
           .WithMany()
           .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Teacher)
           .WithMany()
           .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.StartTime)
           .HasConversion<TimeOnlyConverter, TimeOnlyComparer>();

        builder.Property(x => x.EndTime)
          .HasConversion<TimeOnlyConverter, TimeOnlyComparer>();

    }
}
