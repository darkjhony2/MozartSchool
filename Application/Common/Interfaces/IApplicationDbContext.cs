using ColegioMozart.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ColegioMozart.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<ESubject> Subjects { get; }
    DbSet<EAcademicPeriod> AcademicPeriods { get; }
    DbSet<ETeacher> Teachers { get; }
    DbSet<EPerson> Persons { get; }
    DbSet<EClassRoom> ClassRooms { get; }
    DbSet<EClassSchedule> ClassSchedules { get; }
    DbSet<EAcademicLevel> AcademicLevels { get; }
    DbSet<EAcademicScale> AcademicScales { get; }
    DbSet<EShift> Shifts { get; }
    DbSet<EDocumentType> DocumentTypes { get; }
    DbSet<EGender> Genders { get; }
    DbSet<ESection> Sections { get; }
    DbSet<EStudent> Students { get; }


    DbSet<EEntityFields> EntityFields { get; }
    DbSet<EEntity> Entities { get; }
    DbSet<EView> Views { get; }
    IQueryable GetQueryable(Type type);

    EntityEntry<TEntity> Entry<TEntity>(TEntity entity)
            where TEntity : class;

    EntityEntry<TEntity> Attach<TEntity>(TEntity entity)
            where TEntity : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
