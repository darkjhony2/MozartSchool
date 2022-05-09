using ColegioMozart.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColegioMozart.Infrastructure.Persistence.Configurations.Common;

public class EEntityConfiguration : IEntityTypeConfiguration<EEntity>
{
    public void Configure(EntityTypeBuilder<EEntity> builder)
    {
        builder.ToTable("entities", "framework");

        builder.HasKey(x => x.Id);

        builder
            .HasMany(x=> x.EntityFields)
            .WithOne(e => e.Entity);

        builder.Property(e => e.CreateEntityFullName)
            .HasMaxLength(300);


        builder.Property(e => e.EditEntityFullName)
            .HasMaxLength(300);
    }
}
