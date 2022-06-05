using ColegioMozart.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColegioMozart.Infrastructure.Persistence.Configurations;

public class EEvaluationScoreConfiguration : AuditableEntityMap<EEvaluationScore, Guid>
{
    public override void Configure(EntityTypeBuilder<EEvaluationScore> builder)
    {
        base.Configure(builder);

        builder.ToTable("evaluation_scores", options =>
        {
            options.IsTemporal();
        });


        builder.HasIndex(u => new { u.EvaluationId, u.StudentId })
            .IsUnique();

        builder.Property(x => x.Score)
            .HasColumnType("decimal(10, 4)")
                  .IsRequired();


        builder.Property(x => x.EvaluationId)
                 .IsRequired();

        builder.Property(x => x.StudentId)
            .IsRequired();

        builder.HasOne(x => x.Student)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Evaluation)
           .WithMany()
           .OnDelete(DeleteBehavior.NoAction);

        builder.Property(x => x.Comments)
           .HasMaxLength(150);
    }


}
