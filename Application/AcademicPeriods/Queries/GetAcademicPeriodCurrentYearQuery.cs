namespace ColegioMozart.Application.AcademicPeriods.Queries;

public class GetAcademicPeriodCurrentYearQuery : IRequest<IList<AcademicPeriodDTO>>
{
}


public class GetAcademicPeriodCurrentYearQueryHandler : IRequestHandler<GetAcademicPeriodCurrentYearQuery, IList<AcademicPeriodDTO>>
{
    private readonly ILogger<GetAcademicPeriodCurrentYearQueryHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetAcademicPeriodCurrentYearQueryHandler(
        ILogger<GetAcademicPeriodCurrentYearQueryHandler> logger,
        IApplicationDbContext context,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<IList<AcademicPeriodDTO>> Handle(GetAcademicPeriodCurrentYearQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Se listan los periodos academicos del presente año");

        int currentYear = DateTime.Now.Year;

        var academicPeriods = await _context.AcademicPeriods
            .Where(x => x.Year == currentYear)
            .OrderBy(x => x.StartDate)
            .ProjectTo<AcademicPeriodDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return academicPeriods;
    }
}