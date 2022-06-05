using ColegioMozart.Application.EvaluationTypes.Dtos;

namespace ColegioMozart.Application.EvaluationTypes.Queries;

public class GetAllEvaluationTypesQuery : IRequest<IList<EvaluationTypeDTO>>
{
}

public class GetAllEvaluationTypesQueryHandler : IRequestHandler<GetAllEvaluationTypesQuery, IList<EvaluationTypeDTO>>
{
    private readonly ILogger<GetAllEvaluationTypesQueryHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetAllEvaluationTypesQueryHandler(
        ILogger<GetAllEvaluationTypesQueryHandler> logger,
        IApplicationDbContext context,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<IList<EvaluationTypeDTO>> Handle(GetAllEvaluationTypesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Listar los tipos de evaluación");

        return await _context.EvaluationTypes
            .AsNoTracking()
            .ProjectTo<EvaluationTypeDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}
