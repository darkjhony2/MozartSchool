using ColegioMozart.Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColegioMozart.Infrastructure.Persistence.Configurations;

public class AuditableEntityMap<TEntity, TId> : KeyedEntityMap<TEntity, TId>
    where TEntity : AuditableEntity<TId>
    where TId : struct
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);

        builder.Property(t => t.CreatedBy).HasMaxLength(80);
        builder.Property(t => t.LastModifiedBy).HasMaxLength(80);
    }
}