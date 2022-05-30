namespace ColegioMozart.Application.AcademicLevels.Queries.GetAcademicLevels;

public class GetAcademicLevelsQuery : IRequest<IList<AcademicLevelDTO>>
{


}

public class GetAcademicLevelsQueryHandler : IRequestHandler<GetAcademicLevelsQuery, IList<AcademicLevelDTO>>
{
    private readonly ILogger<GetAcademicLevelsQueryHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAcademicLevelsQueryHandler(
        ILogger<GetAcademicLevelsQueryHandler> logger,
        IApplicationDbContext context,
        IMapper mapper)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
    }

    public async Task<IList<AcademicLevelDTO>> Handle(GetAcademicLevelsQuery request, CancellationToken cancellationToken)
    {
        return await _context.AcademicLevels
            .AsNoTracking()
            .ProjectTo<AcademicLevelDTO>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
