using ColegioMozart.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColegioMozart.Infrastructure.Persistence.Configurations;

public class EStudentConfiguration : AuditableEntityMap<EStudent, Guid>
{

    public override void Configure(EntityTypeBuilder<EStudent> builder)
    {
        base.Configure(builder);

        builder.ToTable("students", options =>
        {
            options.IsTemporal();
        });


        builder.Property(x => x.PersonId)
            .IsRequired();

        builder.HasOne(e => e.CurrentAcademicLevel)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Person)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

    }

}
