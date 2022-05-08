using ColegioMozart.Domain.Entities;
using ColegioMozart.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColegioMozart.Infrastructure.Persistence.Configurations;

public class EPersonConfiguration : AuditableEntityMap<EPerson, Guid>
{

    public override void Configure(EntityTypeBuilder<EPerson> builder)
    {
        base.Configure(builder);

        builder.ToTable("persons", options =>
        {
            options.IsTemporal();
        });


        builder.HasIndex(u => new { u.DocumentTypeId, u.DocumentNumber })
        .IsUnique();

        builder.Property(x => x.DateOfBirth)
         .HasConversion<DateOnlyConverter, DateOnlyComparer>();

        builder.Property(x => x.DocumentNumber)
            .HasMaxLength(20)
            .IsRequired();


        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.MothersLastName)
          .HasMaxLength(100)
          .IsRequired();

        builder.Property(x => x.LastName)
          .HasMaxLength(100)
          .IsRequired();

    }
}
