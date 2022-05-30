using ColegioMozart.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ColegioMozart.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<ESubject> Subjects { get; }
    DbSet<EClassRoom> ClassRooms { get; }
    DbSet<EAcademicLevel> AcademicLevels { get; }
    DbSet<EAcademicScale> AcademicScales { get; }
    DbSet<EShift> Shifts { get; }
    DbSet<ESection> Sections { get; }


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
