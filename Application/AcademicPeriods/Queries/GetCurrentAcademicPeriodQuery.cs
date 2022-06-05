using ColegioMozart.Application.Common.Exceptions;

namespace ColegioMozart.Application.AcademicPeriods.Queries;

public class GetCurrentAcademicPeriodQuery : IRequest<AcademicPeriodDTO>
{
    public DateTime Date { get; set; }
}

public class GetCurrentAcademicPeriodQueryHandler : IRequestHandler<GetCurrentAcademicPeriodQuery, AcademicPeriodDTO>
{
    private readonly ILogger<GetCurrentAcademicPeriodQueryHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetCurrentAcademicPeriodQueryHandler(
        ILogger<GetCurrentAcademicPeriodQueryHandler> logger,
        IApplicationDbContext context,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<AcademicPeriodDTO> Handle(GetCurrentAcademicPeriodQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Find academic period for current date");

        var currentDate = DateOnly.FromDateTime(request.Date);

        var academicPeriod = await _context.AcademicPeriods
            .Where(x => x.StartDate <= currentDate && x.EndDate >= currentDate)
            .ProjectTo<AcademicPeriodDTO>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        if (academicPeriod == null)
        {
            throw new NotFoundException($"No se encontró un periodo académico registrado para el fecha ({currentDate.ToString("dd/MM/yyyy")})");
        }


        return academicPeriod;
    }
}