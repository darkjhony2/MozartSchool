using ColegioMozart.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColegioMozart.Infrastructure.Persistence.Configurations.Common;

public class EEntityFieldsConfiguration : IEntityTypeConfiguration<EEntityFields>
{
    public void Configure(EntityTypeBuilder<EEntityFields> builder)
    {
        builder.ToTable("entity_fields", "framework");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.DisplayName)
           .HasMaxLength(100)
           .IsRequired();

        builder.Property(x => x.Name)
          .HasMaxLength(100)
          .IsRequired();

    }
}
