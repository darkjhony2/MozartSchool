using ColegioMozart.Application.AcademicPeriods.Queries;
using ColegioMozart.Application.AttendanceRecord.Dtos;
using ColegioMozart.Application.AttendanceStatus.Queries;
using ColegioMozart.Application.Common.Exceptions;
using ColegioMozart.Application.Common.Security;
using ColegioMozart.Application.StudentClassroom.Queries.StudentsByClassroom;
using ColegioMozart.Domain.Entities;
using System.Text;

namespace ColegioMozart.Application.AttendanceRecord.Commands;


[Authorize]
public class RegisterAttendanceRecordForClassroomCommand : IRequest
{
    public Guid ClassroomId { get; set; }
    public DateTime Date { get; set; }
    public List<RegisterAttendaceRecordResource> StudentsAttendance { get; set; }
}

public class RegisterAttendanceRecordForClassroomCommandHandler : IRequestHandler<RegisterAttendanceRecordForClassroomCommand>
{
    private readonly ILogger<RegisterAttendanceRecordForClassroomCommandHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;
    private readonly ISender _mediator;

    public RegisterAttendanceRecordForClassroomCommandHandler(
        ILogger<RegisterAttendanceRecordForClassroomCommandHandler> logger,
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

    public async Task<Unit> Handle(RegisterAttendanceRecordForClassroomCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Registrar asistencia para un salón de clases {}", request.ClassroomId);

        var currentAcademicPeriod = await _mediator.Send(new GetCurrentAcademicPeriodQuery() { Date = request.Date });
        var classroomWithStudents = await _mediator.Send(new GetAllStudentsByClassroomId() { ClassroomId = request.ClassroomId });
        var attendanceStatus = await _mediator.Send(new GetAllAttendanceStatusQuery());

        var attendanceStatusIds = request.StudentsAttendance.Select(x => x.AttendanceStatusId).Distinct().ToList();

        if (!attendanceStatusIds.All(attendanceStatus.Select(x => x.Id).Contains))
        {
            throw new NotFoundException("Algún tipo de asistencia no es el correcto.");
        }

        var studentIdsFromRequest = request.StudentsAttendance.Select(x => x.StudentId).ToList();

        var studentIsFromClassroom = classroomWithStudents.Students.Select(x => x.Id).ToList();

        var equalsStudents = studentIdsFromRequest.All(studentIsFromClassroom.Contains) && studentIdsFromRequest.Count == studentIsFromClassroom.Count;

        if (equalsStudents == false)
        {
            var notFoundStudentsIds = studentIsFromClassroom.Where(p => !studentIdsFromRequest.Any(p2 => p2 == p)).ToList();
            var studentsNotFound = classroomWithStudents.Students.Where(x => notFoundStudentsIds.Contains(x.Id)).ToList();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Se debe tomar asistencia a todo el salón.");
            sb.AppendLine("Faltan los alumnos : ");

            foreach (var student in studentsNotFound)
            {
                sb.AppendLine($"\t {student.LastName} {student.MothersLastName} {student.Name}");
            }

            throw new BusinessRuleException(sb.ToString());
        }

        var alreadyRegisterInfo = await _context
            .AttendanceRecords
            .AsNoTracking()
            .Where(x => x.Date == DateOnly.FromDateTime(request.Date) && studentIsFromClassroom.Contains(x.StudentId))
            .ProjectTo<AttendanceRecordDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

        if (alreadyRegisterInfo != null && alreadyRegisterInfo.Count > 0)
        {
            var studentsRegister = alreadyRegisterInfo.Select(x =>
                x.Student.Name + " " + x.Student.LastName + " " + x.Student.MothersLastName + " - " +
                x.AttendanceStatus.Name
                );

            
            throw new BusinessRuleException(
                "No se puede volver a tomar la asistencia. Ya existe un registro de asistencia para este salón." + Environment.NewLine + 
                String.Join(Environment.NewLine + "-", studentsRegister));
        }

        var attendaceRecords = request.StudentsAttendance
            .Select(x =>
            {
                var record = new EAttendanceRecord()
                {
                    StudentId = x.StudentId,
                    Date = DateOnly.FromDateTime(request.Date),
                    AttendanceStatusId = x.AttendanceStatusId,
                    AcademicPeriodId = currentAcademicPeriod.Id,
                    Comments = x.Comments
                };

                return record;
            });

        await _context.AttendanceRecords.AddRangeAsync(attendaceRecords, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
