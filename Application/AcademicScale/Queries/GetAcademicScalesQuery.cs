using ColegioMozart.Application.AcademicScale.Dtos;
using ColegioMozart.Application.Common.Security;

namespace ColegioMozart.Application.AcademicScale.Queries;


[Authorize]
public class GetAcademicScalesQuery : IRequest<IList<AcademicScaleDTO>>
{
}

public class GetAcademicScalesQueryHandler : IRequestHandler<GetAcademicScalesQuery, IList<AcademicScaleDTO>>
{
    private readonly ILogger<GetAcademicScalesQueryHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetAcademicScalesQueryHandler(
        ILogger<GetAcademicScalesQueryHandler> logger,
        IApplicationDbContext context,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<IList<AcademicScaleDTO>> Handle(GetAcademicScalesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Se listan las escalas academicas");

        return await _context.AcademicScales
            .ProjectTo<AcademicScaleDTO>(_mapper.ConfigurationProvider)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }
}
