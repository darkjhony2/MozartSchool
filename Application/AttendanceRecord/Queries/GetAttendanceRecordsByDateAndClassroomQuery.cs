using ColegioMozart.Application.AttendanceRecord.Dtos;
using ColegioMozart.Application.Common.Security;

namespace ColegioMozart.Application.AttendanceRecord.Queries;


[Authorize]
public class GetAttendanceRecordsByDateAndClassroomQuery : IRequest<IList<AttendanceRecordDTO>>
{
    public DateTime Date { get; set; }
    public Guid ClassroomId { get; set; }
}

public class GetAttendanceRecordsByDateAndClassroomQueryHandler : IRequestHandler<GetAttendanceRecordsByDateAndClassroomQuery, IList<AttendanceRecordDTO>>
{
    private readonly ILogger<GetAttendanceRecordsByDateAndClassroomQueryHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetAttendanceRecordsByDateAndClassroomQueryHandler(
        ILogger<GetAttendanceRecordsByDateAndClassroomQueryHandler> logger,
        IApplicationDbContext context,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<IList<AttendanceRecordDTO>> Handle(GetAttendanceRecordsByDateAndClassroomQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Find attendance record by date and classroom @{request}", request);

        return await _context.AttendanceRecords
            .Where(x => x.Date == DateOnly.FromDateTime(request.Date) && x.Student.ClassRoomId == request.ClassroomId)
            .OrderBy(x => x.Student.Person.LastName)
            .ThenBy(x => x.Student.Person.MothersLastName)
            .ThenBy(x => x.Student.Person.Name)
            .ProjectTo<AttendanceRecordDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}