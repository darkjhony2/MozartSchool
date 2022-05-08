using ColegioMozart.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ColegioMozart.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<ESubject> Subjects { get; }
    DbSet<EAcademicLevel> AcademicLevels { get; }
    DbSet<EShift> Shifts { get; }
    DbSet<ESection> Sections { get; }


    DbSet<EEntityFields> EntityFields { get; }
    DbSet<EEntity> Entities { get; }
    DbSet<EView> Views { get; }
    IQueryable GetQueryable(Type type);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
