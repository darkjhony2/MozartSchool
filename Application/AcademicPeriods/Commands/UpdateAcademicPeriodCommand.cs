using ColegioMozart.Application.Common.Exceptions;

namespace ColegioMozart.Application.AcademicPeriods.Commands;

public class UpdateAcademicPeriodCommand : IRequest
{
    public Guid Id { get; set; }
    public UpdateAcademicPeriodDTO Resource { get; set; }
}


public class UpdateAcademicPeriodCommandHandler : IRequestHandler<UpdateAcademicPeriodCommand>
{
    private readonly ILogger<UpdateAcademicPeriodCommandHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public UpdateAcademicPeriodCommandHandler(
        ILogger<UpdateAcademicPeriodCommandHandler> logger,
        IApplicationDbContext context,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(UpdateAcademicPeriodCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Actualizar periodo académico : @{resource}", request.Resource);
        int currentYear = DateTime.Now.Year;

        var entity = await _context.AcademicPeriods.Where(x => x.Id == request.Id).FirstOrDefaultAsync();

        if (entity == null)
        {
            throw new NotFoundException("Periodo académico", request.Id);
        }

        if (entity.Year != currentYear)
        {
            throw new BusinessRuleException("Solo se pueden actualizar registros del presente año.");
        }

        if(await _context
            .AcademicPeriods
            .Where(x => x.Year == currentYear && x.Id != entity.Id && x.Name == request.Resource.Name)
            .AnyAsync())
        {
            throw new BusinessRuleException("Ya existe otro periodo academico en el mismo año con el nombre a actualizar.");
        }

        entity.StartDate = DateOnly.FromDateTime(request.Resource.StartDate);
        entity.EndDate = DateOnly.FromDateTime(request.Resource.EndDate);

        entity.Name = request.Resource.Name;

        if (await _context
            .AcademicPeriods
            .Where(x => (x.StartDate >= DateOnly.FromDateTime(request.Resource.StartDate) && x.StartDate <= DateOnly.FromDateTime(request.Resource.EndDate)
            || x.EndDate >= DateOnly.FromDateTime(request.Resource.StartDate) && x.EndDate <= DateOnly.FromDateTime(request.Resource.EndDate))
             && x.Id != entity.Id)
            .AnyAsync())
        {
            throw new BusinessRuleException("No se puede actualizar : La fecha de inicio o fin se cruza con otro periodo del presente año.");
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}