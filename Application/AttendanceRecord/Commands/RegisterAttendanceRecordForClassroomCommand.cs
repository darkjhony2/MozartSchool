using ColegioMozart.Application.AcademicPeriods.Queries;
using ColegioMozart.Application.AttendanceRecord.Dtos;
using ColegioMozart.Application.Common.Exceptions;
using ColegioMozart.Domain.Entities;

namespace ColegioMozart.Application.AttendanceRecord.Commands;

public class RegisterAttendanceRecordForClassroomCommand : IRequest
{
    public Guid ClassroomId { get; set; }
    public DateTime Date { get; set; }
    public List<RegisterAttendaceRecordForClassroomResource> StudentsAttendance { get; set; }
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

        var currentAcademicPeriod = await _mediator.Send(new GetCurrentAcademicPeriodQuery());

        var classroom = await _context.ClassRooms.Where(x => x.Id == request.ClassroomId).FirstOrDefaultAsync();

        if (classroom == null)
        {
            throw new NotFoundException("Salón de clases", request.ClassroomId);
        }

        //Pendiete validaciones
        //-Usuarios repetidos en array
        //-Registro de asistencia para un alumno ya registrado
        //-Validar que todos los usuarios existan
        //-Validar que todos los estado de asistencia existan

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
