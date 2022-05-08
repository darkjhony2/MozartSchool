using ColegioMozart.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColegioMozart.Infrastructure.Persistence.Configurations;

internal class ESubjectConfiguration : AuditableEntityMap<ESubject, Guid>
{
    public override void Configure(EntityTypeBuilder<ESubject> builder)
    {
        base.Configure(builder);

        builder.ToTable("subjects", options =>
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
