using ColegioMozart.Application.ClassRoom.Queries;
using ColegioMozart.Application.ClassSchedule.Dtos;

namespace ColegioMozart.Application.ClassSchedule.Queries;

public class GetClassScheduleByStudentIdQuery : IRequest<IList<ClassScheduleForStudentDTO>>
{
    public Guid StudentId { get; set; }
    public int Year { get; set; }
}

public class GetClassScheduleByStudentIdQueryHandler : IRequestHandler<GetClassScheduleByStudentIdQuery, IList<ClassScheduleForStudentDTO>>
{
    private readonly ILogger<GetClassScheduleByStudentIdQueryHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;
    private readonly ISender _mediator;

    public GetClassScheduleByStudentIdQueryHandler(
        ILogger<GetClassScheduleByStudentIdQueryHandler> logger,
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

    public async Task<IList<ClassScheduleForStudentDTO>> Handle(GetClassScheduleByStudentIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Get student class schedule by studentId {} for year {}", request.StudentId, request.Year);

        var classroom = await _mediator.Send(new GetClassroomByStudentIdQuery { Year = request.Year, StudentId = request.StudentId });

        return await _context
            .ClassSchedules
            .Where(x => x.ClassRoomId == classroom.Id)    
            .OrderBy(x => x.DayOfWeek).ThenBy(x => x.StartTime)
            .ProjectTo<ClassScheduleForStudentDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}

