using ColegioMozart.Application.AcademicPeriods.Queries;
using ColegioMozart.Application.AttendanceRecord.Dtos;
using ColegioMozart.Application.AttendanceStatus.Queries;
using ColegioMozart.Application.Common.Exceptions;
using ColegioMozart.Application.Students.Queries;

namespace ColegioMozart.Application.AttendanceRecord.Commands;

public class UpdateAttendanceRecordForStudentCommand : IRequest
{
    public DateTime Date { get; set; }
    public RegisterAttendaceRecordResource Resource { get; set; }
}

public class UpdateAttendanceRecordForStudentCommandHandler : IRequestHandler<UpdateAttendanceRecordForStudentCommand>
{
    private readonly ILogger<UpdateAttendanceRecordForStudentCommandHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;
    private readonly ISender _mediator;

    public UpdateAttendanceRecordForStudentCommandHandler(
        ILogger<UpdateAttendanceRecordForStudentCommandHandler> logger,
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

    public async Task<Unit> Handle(UpdateAttendanceRecordForStudentCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Actualizar asistencia para un alumno : @{request}", request);

        await _mediator.Send(new GetCurrentAcademicPeriodQuery() { Date = request.Date });
        var attendanceStatus = await _mediator.Send(new GetAttendanceStatusByIdQuery { Id = request.Resource.AttendanceStatusId });

        var attendanceRecordForStudent = await _context.AttendanceRecords
            .Where(x => x.Date == DateOnly.FromDateTime(request.Date) && x.StudentId == request.Resource.StudentId)
            .FirstOrDefaultAsync();


        if (attendanceRecordForStudent == null)
        {
            var student = await _mediator.Send(new GetStudentByIdQuery { StudentId = request.Resource.StudentId });

            throw new NotFoundException($"No se encontró el registro de asitencia para el alumno ({student.Person.LastName} {student.Person.MothersLastName} {student.Person.Name}) para el día {request.Date.ToString("dd/MM/yyyy")}");
        }

        attendanceRecordForStudent.AttendanceStatusId = request.Resource.AttendanceStatusId;
        attendanceRecordForStudent.Comments = request.Resource.Comments;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Se actualizó el registro satisfactoriamente @{attendanceRecordForStudent}", attendanceRecordForStudent);

        return Unit.Value;
    }
}
