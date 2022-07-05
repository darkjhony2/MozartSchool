using ColegioMozart.Application.Common.Security;
using ColegioMozart.Application.Evaluations.Dtos;

namespace ColegioMozart.Application.Evaluations.Queries;

[Authorize()]
public class GetEvaluationByTeacherQuery : IRequest<List<EvaluationResource>>
{
    public Guid? Id { get; set; }
}


public class GetEvaluationByTeacherQueryHandler : IRequestHandler<GetEvaluationByTeacherQuery, List<EvaluationResource>>
{
    private readonly ILogger<GetEvaluationByTeacherQueryHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetEvaluationByTeacherQueryHandler(
        ILogger<GetEvaluationByTeacherQueryHandler> logger,
        IApplicationDbContext context,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<List<EvaluationResource>> Handle(GetEvaluationByTeacherQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Obtener evaluaciones por docente @{request}", request);

        var idTeacher = request.Id ?? await _context.Teachers.Where(x => x.Person.UserId == _currentUserService.UserId).Select(x => x.Id).FirstAsync();

        return await _context
            .Evaluations
            .AsNoTracking()
            .Where(x => x.TeacherId == idTeacher && x.AcademicPeriod.Year == DateTime.Now.Year)
            .ProjectTo<EvaluationResource>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}