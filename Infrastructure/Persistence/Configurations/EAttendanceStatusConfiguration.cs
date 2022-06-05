using ColegioMozart.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColegioMozart.Infrastructure.Persistence.Configurations;

public class EAttendanceStatusConfiguration : AuditableEntityMap<EAttendanceStatus, int>
{
    public override void Configure(EntityTypeBuilder<EAttendanceStatus> builder)
    {
        base.Configure(builder);

        builder.ToTable("attendace_status", options =>
        {

        });


        builder.HasIndex(u => u.Name)
        .IsUnique();

        builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Abbreviation)
           .HasMaxLength(5)
           .IsRequired();

    }


}
