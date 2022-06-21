using ColegioMozart.Application.Common.Exceptions;
using ColegioMozart.Domain.Entities;

namespace ColegioMozart.Application.AcademicPeriods.Commands;

public class CreateAcademicPeriodCommand : IRequest
{
    public CreateAcademicPeriodDTO Resource;
}

public class CreateAcademicPeriodCommandHandler : IRequestHandler<CreateAcademicPeriodCommand>
{
    private readonly ILogger<CreateAcademicPeriodCommandHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public CreateAcademicPeriodCommandHandler(
        ILogger<CreateAcademicPeriodCommandHandler> logger,
        IApplicationDbContext context,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(CreateAcademicPeriodCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Crear nuevo periodo academico para el año actual");

        var repetead = await _context
             .AcademicPeriods
             .Where(x => x.StartDate >= DateOnly.FromDateTime(request.Resource.StartDate) && x.StartDate <= DateOnly.FromDateTime(request.Resource.EndDate)
             || x.EndDate >= DateOnly.FromDateTime(request.Resource.StartDate) && x.EndDate <= DateOnly.FromDateTime(request.Resource.EndDate))
             .FirstOrDefaultAsync();

        if (repetead != null)
        {
            _logger.LogInformation("Registro repetido : {0} - {1}", repetead.Id, repetead.Name);
            throw new EntityAlreadyExistException(new HashSet<string> { "StartDate", "EndDate" });
        }

        var academicPeriod = new EAcademicPeriod()
        {
            Year = DateTime.Now.Year,
            StartDate = DateOnly.FromDateTime(request.Resource.StartDate),
            EndDate = DateOnly.FromDateTime(request.Resource.EndDate),
            Name = request.Resource.Name
        };


        _context.AcademicPeriods.Add(academicPeriod);

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Se creó un nuevo nivel academico con id: {id}", academicPeriod.Id);

        return Unit.Value;
    }
}
