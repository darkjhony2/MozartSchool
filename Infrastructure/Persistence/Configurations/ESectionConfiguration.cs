using ColegioMozart.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColegioMozart.Infrastructure.Persistence.Configurations;

public class ESectionConfiguration : AuditableEntityMap<ESection, int>
{
    public override void Configure(EntityTypeBuilder<ESection> builder)
    {
        base.Configure(builder);

        builder.ToTable("sections", options =>
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
