using ColegioMozart.Application.AcademicLevels;
using ColegioMozart.Application.Common.Exceptions;

namespace ColegioMozart.Application.AcademicScale.Queries;

public class GetAcademicLevelByAcademicScaleId : IRequest<IList<AcademicLevelDTO>>
{
    public int AcademicScaleId { get; set; }
}

public class GetAcademicLevelByAcademicScaleIdHandler : IRequestHandler<GetAcademicLevelByAcademicScaleId, IList<AcademicLevelDTO>>
{
    private readonly ILogger<GetAcademicLevelByAcademicScaleIdHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetAcademicLevelByAcademicScaleIdHandler(
        ILogger<GetAcademicLevelByAcademicScaleIdHandler> logger,
        IApplicationDbContext context,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<IList<AcademicLevelDTO>> Handle(GetAcademicLevelByAcademicScaleId request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Listar niveles academicos por escala {}", request.AcademicScaleId);

        if (!await _context.AcademicScales.Where(x => x.Id == request.AcademicScaleId).AnyAsync())
        {
            throw new NotFoundException("Escala académica", request.AcademicScaleId);
        }

        var academicLevels = await _context.AcademicLevels
            .Where(x => x.AcademicScaleId == request.AcademicScaleId)
            .ProjectTo<AcademicLevelDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return academicLevels;
    }
}
