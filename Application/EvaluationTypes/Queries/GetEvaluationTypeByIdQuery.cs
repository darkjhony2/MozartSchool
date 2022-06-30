using ColegioMozart.Application.Common.Exceptions;
using ColegioMozart.Application.Common.Security;
using ColegioMozart.Application.EvaluationTypes.Dtos;

namespace ColegioMozart.Application.EvaluationTypes.Queries;

[Authorize]
public class GetEvaluationTypeByIdQuery : IRequest<EvaluationTypeDTO>
{
    public int Id { get; set; }
}
public class GetEvaluationTypeByIdQueryHandler : IRequestHandler<GetEvaluationTypeByIdQuery, EvaluationTypeDTO>
{
    private readonly ILogger<GetEvaluationTypeByIdQueryHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetEvaluationTypeByIdQueryHandler(
        ILogger<GetEvaluationTypeByIdQueryHandler> logger,
        IApplicationDbContext context,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<EvaluationTypeDTO> Handle(GetEvaluationTypeByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Buscar tipo de evaluación por Id : {}", request.Id);

        var entity = await _context.EvaluationTypes
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .ProjectTo<EvaluationTypeDTO>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        if (entity == null)
        {
            throw new NotFoundException("Tipo de evaluación", request.Id);
        }

        return entity;
    }
}
