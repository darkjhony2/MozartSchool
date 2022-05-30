using ColegioMozart.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioMozart.Infrastructure.Persistence.Configurations
{
    internal class EClassRoomConfiguration : AuditableEntityMap<EClassRoom, Guid>
    {
        public override void Configure(EntityTypeBuilder<EClassRoom> builder)
        {
            base.Configure(builder);

            builder.ToTable("classrooms", options =>
            {
                options.IsTemporal();
            });

            builder.Property(x => x.Year)
                .IsRequired();
            

            builder.Property(x => x.LevelId)
                .IsRequired();

            builder.HasOne(e => e.Level)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.ShiftId)
                .IsRequired();

            builder.HasOne(e => e.Shift)
               .WithMany()
               .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.TutorId)
                .IsRequired();

            builder.HasOne(e => e.Tutor)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.SectionId)
                .IsRequired();

            builder.HasOne(e => e.Section)
               .WithMany()
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.Students)
                .WithOne(e => e.ClassRoom)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
