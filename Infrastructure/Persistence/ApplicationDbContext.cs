using System.Reflection;
using ColegioMozart.Application.Common.Interfaces;
using ColegioMozart.Domain.Common;
using ColegioMozart.Domain.Entities;
using ColegioMozart.Infrastructure.Identity;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ColegioMozart.Infrastructure.Persistence;

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTime _dateTime;
    private readonly IDomainEventService _domainEventService;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IOptions<OperationalStoreOptions> operationalStoreOptions,
        ICurrentUserService currentUserService,
        IDomainEventService domainEventService,
        IDateTime dateTime) : base(options, operationalStoreOptions)
    {
        _currentUserService = currentUserService;
        _domainEventService = domainEventService;
        _dateTime = dateTime;
    }

    public DbSet<ESubject> Subjects => Set<ESubject>();
    public DbSet<EShift> Shifts => Set<EShift>();
    public DbSet<EAcademicLevel> AcademicLevels => Set<EAcademicLevel>();
    public DbSet<EAcademicScale> AcademicScales => Set<EAcademicScale>();
    public DbSet<ESection> Sections => Set<ESection>();
    public DbSet<EAcademicPeriod> AcademicPeriods => Set<EAcademicPeriod>();

    public DbSet<ETeacher> Teachers => Set<ETeacher>();


    public DbSet<EEntityFields> EntityFields => Set<EEntityFields>();
    public DbSet<EEntity> Entities => Set<EEntity>();
    public DbSet<EView> Views => Set<EView>();

    public IQueryable GetQueryable(Type type)
    {
        var method = typeof(DbContext).GetMethods().Where(x => x.Name == "Set").FirstOrDefault();

        var setMethod = method.MakeGenericMethod(type);

        dynamic querable = setMethod.Invoke((DbContext)this, null);

        return querable
        .AsQueryable();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        /*foreach (var entry in ChangeTracker.Entries<AuditableEntity<T>>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _currentUserService.UserId;
                    entry.Entity.Created = _dateTime.Now;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = _currentUserService.UserId;
                    entry.Entity.LastModified = _dateTime.Now;
                    break;
            }
        }*/

        var events = ChangeTracker.Entries<IHasDomainEvent>()
                .Select(x => x.Entity.DomainEvents)
                .SelectMany(x => x)
                .Where(domainEvent => !domainEvent.IsPublished)
                .ToArray();

        var result = await base.SaveChangesAsync(cancellationToken);

        await DispatchEvents(events);

        return result;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    private async Task DispatchEvents(DomainEvent[] events)
    {
        foreach (var @event in events)
        {
            @event.IsPublished = true;
            await _domainEventService.Publish(@event);
        }
    }
}
