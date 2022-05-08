using ColegioMozart.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColegioMozart.Infrastructure.Persistence.Configurations.Common;

public class EViewConfiguration : IEntityTypeConfiguration<EView>
{

    public void Configure(EntityTypeBuilder<EView> builder)
    {
        builder.ToTable("views", "framework");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.DisplayName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Name)
           .HasMaxLength(100)
           .IsRequired();

    }

}
