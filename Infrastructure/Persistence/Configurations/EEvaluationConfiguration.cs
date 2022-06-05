using ColegioMozart.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColegioMozart.Infrastructure.Persistence.Configurations;

public class EEvaluationConfiguration : AuditableEntityMap<EEvaluation, Guid>
{
    public override void Configure(EntityTypeBuilder<EEvaluation> builder)
    {
        base.Configure(builder);

        builder.ToTable("evaluations", options =>
        {
            options.IsTemporal();
        });


        builder.HasIndex(u => new { u.ClassRoomId, u.AcademicPeriodId, u.TeacherId, u.EvaluationDate, u.SubjectId })
            .IsUnique();

        builder.Property(x => x.EvaluationTypeId)
            .IsRequired();

        builder.Property(x => x.EvaluationName)
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(x => x.SubjectId)
           .IsRequired();

        builder.Property(x => x.ClassRoomId)
          .IsRequired();

        builder.Property(x => x.TeacherId)
          .IsRequired();

        builder.Property(x => x.Weight)
            .HasColumnType("decimal(10, 4)")
         .IsRequired();

        builder.Property(x => x.EvaluationDate)
        .IsRequired();

        builder.Property(x => x.MaxEditDate)
       .IsRequired();

        builder.Property(x => x.MaximumScore)
            .HasColumnType("decimal(10, 4)")
           .IsRequired();


        builder.HasOne(x => x.EvaluationType)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Subject)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasOne(x => x.AcademicPeriod)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ClassRoom)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Teacher)
           .WithMany()
           .OnDelete(DeleteBehavior.Restrict);
    }

}
