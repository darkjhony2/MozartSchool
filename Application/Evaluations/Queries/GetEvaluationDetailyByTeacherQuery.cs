using ColegioMozart.Application.Common.Exceptions;
using ColegioMozart.Application.Common.Security;
using ColegioMozart.Application.Evaluations.Dtos;

namespace ColegioMozart.Application.Evaluations.Queries;

[Authorize()]
public class GetEvaluationDetailyByTeacherQuery : IRequest<EvaluationDetailDTO>
{
    public Guid Id { get; set; }

}

public class GetEvaluationDetailyByTeacherQueryHandler : IRequestHandler<GetEvaluationDetailyByTeacherQuery, EvaluationDetailDTO>
{
    private readonly ILogger<GetEvaluationDetailyByTeacherQueryHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetEvaluationDetailyByTeacherQueryHandler(
        ILogger<GetEvaluationDetailyByTeacherQueryHandler> logger,
        IApplicationDbContext context,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<EvaluationDetailDTO> Handle(GetEvaluationDetailyByTeacherQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Listando el detalle de una evaluacion : {0}", request.Id);

        var idTeacher = await _context.Teachers.Where(x => x.Person.UserId == _currentUserService.UserId).Select(x => x.Id).FirstAsync();

        var evaluation = await _context
            .Evaluations
            .AsNoTracking()
            .Where(x => x.TeacherId == idTeacher && x.AcademicPeriod.Year == DateTime.Now.Year && x.Id == request.Id)
            .ProjectTo<EvaluationResource>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        if (evaluation == null)
        {
            throw new NotFoundException($"No se encontró la evaluación con Id : {request.Id}");
        }


        var scores = await _context.EvaluationScores
            .AsNoTracking()
            .Where(x => x.EvaluationId == request.Id)
            .ProjectTo<EvaluationScoreDetailDto>(_mapper.ConfigurationProvider)
            .ToListAsync();


        if(scores == null)
        {
            throw new NotFoundException($"La evaluacion '{evaluation.EvaluationName}' no cuenta con notas registradas");
        }

        var response = new EvaluationDetailDTO
        {
            Evaluation = evaluation,
            Scores = scores
        };


        return response;
    }
}
