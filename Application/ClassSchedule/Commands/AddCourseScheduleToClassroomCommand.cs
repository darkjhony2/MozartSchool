using ColegioMozart.Application.ClassSchedule.Queries;
using ColegioMozart.Application.Common.Exceptions;
using ColegioMozart.Application.Common.Security;
using ColegioMozart.Domain.Entities;

namespace ColegioMozart.Application.ClassSchedule.Commands;


[Authorize]
public class AddCourseScheduleToClassroomCommand : IRequest
{
    public Guid ClassroomId { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public Guid SubjectId { get; set; }
    public Guid TeacherId { get; set; }
}

public class AddCourseScheduleToClassroomCommandHandler : IRequestHandler<AddCourseScheduleToClassroomCommand>
{
    private readonly ILogger<AddCourseScheduleToClassroomCommandHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;
    private readonly ISender _mediator;

    public AddCourseScheduleToClassroomCommandHandler(
        ILogger<AddCourseScheduleToClassroomCommandHandler> logger,
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

    public async Task<Unit> Handle(AddCourseScheduleToClassroomCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Add Course to class_schedule for classroom {}", request.ClassroomId);
        int currentYear = DateTime.Now.Year;

        if (!await _context.ClassRooms.Where(x => x.Id == request.ClassroomId && x.Year == currentYear).AnyAsync())
        {
            throw new BusinessRuleException("No se encontró el salón o no pertenece al año actual");
        }

        if (!await _context.Subjects.Where(x => x.Id == request.SubjectId).AnyAsync())
        {
            throw new NotFoundException("Materia", request.SubjectId);
        }

        if (!await _context.Teachers.Where(x => x.Id == request.TeacherId).AnyAsync())
        {
            throw new NotFoundException("Docente", request.TeacherId);
        }

        var teacherClassSchedule = await _mediator.Send(new GetClassScheduleByTeacherIdQuery
        {
            TeacherId = request.TeacherId,
            Year = currentYear
        });


        var teacherClassScheduleCollides = teacherClassSchedule.Where(x => x.DayOfWeek == request.DayOfWeek &&
            (x.StartTime > request.StartTime && x.StartTime < request.EndTime
             || x.EndTime > request.StartTime && x.EndTime < request.EndTime)).FirstOrDefault();

        if (teacherClassScheduleCollides != null)
        {
            var tC = teacherClassScheduleCollides.ClassRoom;
            var course = teacherClassScheduleCollides.Subject;
            throw new BusinessRuleException($"No se puede registrar en el horario indicado ya que el docente dicta clases en el mismo horario." +
                $"({tC.Level.Level} Sección '{tC.Section.Name}' {tC.Shift.Name} - Curso : {course.Name})");
        }

        var classroomScheduleCollides = await _context.ClassSchedules
            .Where(x => x.ClassRoomId == request.ClassroomId
             && x.DayOfWeek == request.DayOfWeek
             && (
                x.StartTime > request.StartTime && x.StartTime < request.EndTime
                || x.EndTime > request.StartTime && x.EndTime < request.EndTime
             ))
            .Include(x => x.Subject)
            .FirstOrDefaultAsync();

        if (classroomScheduleCollides != null)
        {
            var t = classroomScheduleCollides;
            throw new BusinessRuleException("No se puede registrar ya que el salón ya cuenta con un curso en el mismo horario " +
                $"({t.StartTime.ToString("hh:mm.tt")} - {t.EndTime.ToString("hh:mm.tt")} {t.Subject.Name}).");
        }

        var schedule = new EClassSchedule
        {
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            DayOfWeek = request.DayOfWeek,
            SubjectId = request.SubjectId,
            ClassRoomId = request.ClassroomId,
            TeacherId = request.TeacherId,
        };

        await _context.ClassSchedules.AddAsync(schedule, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Saves success schedule @{schedule}", schedule);

        return Unit.Value;
    }
}