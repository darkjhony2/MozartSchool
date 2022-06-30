using ColegioMozart.Application.AttendanceRecord.Dtos;
using ColegioMozart.Application.Common.Exceptions;
using ColegioMozart.Application.Common.Security;

namespace ColegioMozart.Application.AttendanceRecord.Queries;


[Authorize]
public class GetAttendanceRecordByAcademicPeriodAndStudentQuery : IRequest<IList<AttendanceRecordForStudentDTO>>
{
    public Guid AcademicPeriodId { get; set; }
    public Guid StudentId { get; set; }
}


public class GetAttendanceRecordByAcademicPeriodAndStudentQueryHandler
    : IRequestHandler<GetAttendanceRecordByAcademicPeriodAndStudentQuery, IList<AttendanceRecordForStudentDTO>>
{
    private readonly ILogger<GetAttendanceRecordByAcademicPeriodAndStudentQueryHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetAttendanceRecordByAcademicPeriodAndStudentQueryHandler(
        ILogger<GetAttendanceRecordByAcademicPeriodAndStudentQueryHandler> logger,
        IApplicationDbContext context,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<IList<AttendanceRecordForStudentDTO>> Handle(GetAttendanceRecordByAcademicPeriodAndStudentQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Registro de asistencia del estudiante {} para el periodo {}", request.StudentId, request.AcademicPeriodId);

        if (!await _context.AcademicPeriods.Where(x => x.Id == request.AcademicPeriodId).AnyAsync())
        {
            throw new NotFoundException("Periodo academico", request.AcademicPeriodId);
        }

        if (!await _context.Students.Where(x => x.Id == request.StudentId).AnyAsync())
        {
            throw new NotFoundException("Estudiante", request.StudentId);
        }

        return await _context.AttendanceRecords
            .Where(x => x.AcademicPeriodId == request.AcademicPeriodId && x.StudentId == request.StudentId)
            .OrderBy(x => x.Date)
            .ProjectTo<AttendanceRecordForStudentDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}