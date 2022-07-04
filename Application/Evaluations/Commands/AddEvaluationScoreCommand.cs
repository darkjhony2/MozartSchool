using ColegioMozart.Application.Common.Exceptions;
using ColegioMozart.Application.Evaluations.Dtos;
using ColegioMozart.Application.StudentClassroom.Queries.StudentsByClassroom;
using ColegioMozart.Domain.Entities;

namespace ColegioMozart.Application.Evaluations.Commands;

public class AddEvaluationScoreCommand : IRequest
{
    public Guid EvaluationId { get; set; }

    public List<AddEvaluationScoreResource> Scores { get; set; }

}

public class AddEvaluationScoreCommandHandler : IRequestHandler<AddEvaluationScoreCommand>
{
    private readonly ILogger<AddEvaluationScoreCommandHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;
    private readonly ISender _mediator;

    public AddEvaluationScoreCommandHandler(
        ILogger<AddEvaluationScoreCommandHandler> logger,
        IApplicationDbContext context,
        IMapper mapper,
        ICurrentUserService currentUserService,
        ISender mediator)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
        _mediator = mediator;
    }


    public async Task<Unit> Handle(AddEvaluationScoreCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Registrando las notas para la evaluacion");
        var teacherUserId = _currentUserService.UserId;
        var teacherId = await _context.Teachers.Where(x => x.Person.UserId == teacherUserId).Select(x => x.Id).FirstOrDefaultAsync();

        var evaluation = await _context.Evaluations.Where(x => x.TeacherId == teacherId && x.Id == request.EvaluationId).FirstOrDefaultAsync();

        if (evaluation == null)
        {
            throw new NotFoundException("No se encontró la evaluación con Id : " + request.EvaluationId);
        }

        if (evaluation.MaxEditDate >= DateTime.Now)
        {
            throw new BusinessRuleException($"No se puede registrar notas debido a qué paso la fecha de edición máxima del examen. ({evaluation.MaxEditDate})");
        }

        var students = await _mediator.Send(new GetAllStudentsByClassroomId { ClassroomId = evaluation.ClassRoomId });
        var requestUserIds = request.Scores.Select(x => x.StudentId);

        CheckStudents(students, requestUserIds);
        CheckMaxScore(request.Scores.Select(x => x.Score), evaluation.MaximumScore);

        var entityScores = request.Scores.Select(x =>
        {
            return new EEvaluationScore
            {
                EvaluationId = request.EvaluationId,
                StudentId = x.StudentId,
                Score = x.Score,
                Comments = x.Comments
            };
        }).ToList();

        _context.EvaluationScores.AddRange(entityScores);

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Se registraron correctamente las notas para la evaluacion {0}", request.EvaluationId);

        return Unit.Value;
    }

    private void CheckMaxScore(IEnumerable<decimal> evaluationScores, decimal maximumScore)
    {
        _logger.LogInformation("Evaluando scores");

        if (evaluationScores.Where(x => x < 0).Any())
        {
            throw new BusinessRuleException("Las notas de las evaluaciones no pueden ser menor a cero");
        }

        if (evaluationScores.Where(x => x > maximumScore).Any())
        {
            throw new BusinessRuleException($"La nota máxima de la evaluación es de {maximumScore}. Usted intentó registrar una nota mayor a la permitida.");
        }
    }

    public void CheckStudents(StudentClassroomDTO dbStudentData, IEnumerable<Guid> requestStudents)
    {
        var studentsIds = dbStudentData.Students.Select(x => x.Id);
        var allOfList1IsInList2 = requestStudents.Intersect(studentsIds).Count() == requestStudents.Count();

        if (!allOfList1IsInList2)
        {
            var dataStudents = string.Join(Environment.NewLine + "-", dbStudentData.Students.Select(x => x.Name + " " + x.LastName + " " + x.MothersLastName).ToList());
            throw new BusinessRuleException($"No se pudo registrar las notas debido a qué no se ingresaron las notas de todos los alumnos del salón o se encuentra alguno repetido {Environment.NewLine} {dataStudents}");
        }

    }
}