using ColegioMozart.Application.Common.Exceptions;
using ColegioMozart.Application.Evaluations.Dtos;
using ColegioMozart.Domain.Entities;

namespace ColegioMozart.Application.Evaluations.Commands;

[Authorize(Roles = "Docente")]
public class AddEvaluationToClassroomCommand : IRequest
{
    public AddEvaluationResource Resource { get; set; }
}

public class AddEvaluationToClassroomCommandHandler : IRequestHandler<AddEvaluationToClassroomCommand>
{
    private readonly ILogger<AddEvaluationToClassroomCommandHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;
    private readonly ISender _mediator;

    public AddEvaluationToClassroomCommandHandler(
        ILogger<AddEvaluationToClassroomCommandHandler> logger,
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

    public async Task<Unit> Handle(AddEvaluationToClassroomCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Crear nueva evaluación");

        var teacherUserId = _currentUserService.UserId;

        var teacherId = await _context.Teachers.Where(x => x.Person.UserId == teacherUserId).Select(x => x.Id).FirstOrDefaultAsync();

        var currentEvaluations = await _context.Evaluations.Where(
                x => x.TeacherId == teacherId &&
                x.AcademicPeriodId == request.Resource.AcademicPeriodId &&
                x.ClassRoomId == request.Resource.ClassRoomId &&
                x.SubjectId == request.Resource.SubjectId).ToListAsync();

        var sumWeigth = currentEvaluations.Sum(x => x.Weight);

        if (sumWeigth + request.Resource.Weight > 1)
        {
            throw new BusinessRuleException($"La suma de los pesos de las evaluaciones no puede superar el valor : 1. \n La suma de los puntajes actuales de : {sumWeigth}");
        }

        var evaluation = new EEvaluation
        {
            EvaluationTypeId = request.Resource.EvaluationTypeId,
            EvaluationName = request.Resource.EvaluationName,
            SubjectId = request.Resource.SubjectId,
            AcademicPeriodId = request.Resource.AcademicPeriodId,
            ClassRoomId = request.Resource.ClassRoomId,
            TeacherId = teacherId,
            Weight = request.Resource.Weight,
            EvaluationDate = request.Resource.EvaluationDate,
            MaxEditDate = request.Resource.EvaluationDate.AddDays(7),
            MaximumScore = request.Resource.MaximumScore
        };

        _context.Evaluations.Add(evaluation);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
