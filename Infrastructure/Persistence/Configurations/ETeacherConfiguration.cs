using ColegioMozart.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColegioMozart.Infrastructure.Persistence.Configurations;

public class ETeacherConfiguration : AuditableEntityMap<ETeacher, Guid>
{
    public override void Configure(EntityTypeBuilder<ETeacher> builder)
    {
        base.Configure(builder);

        builder.ToTable("teachers", options =>
        {
            options.IsTemporal();
        });

    
        builder.Property(x => x.Email)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Phone)
           .HasMaxLength(15);

        builder.Property(x => x.PersonId)
            .IsRequired();

        builder.HasOne(e => e.Person)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

    }
}
