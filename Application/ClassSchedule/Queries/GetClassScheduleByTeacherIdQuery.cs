using ColegioMozart.Application.ClassSchedule.Dtos;
using ColegioMozart.Application.Common.Security;

namespace ColegioMozart.Application.ClassSchedule.Queries;


[Authorize]
public class GetClassScheduleByTeacherIdQuery : IRequest<IList<ClassScheduleForTeacherDTO>>
{
    public Guid TeacherId { get; set; }
    public int Year { get; set; }
}

public class GetClassScheduleByTeacherIdQueryHandler : IRequestHandler<GetClassScheduleByTeacherIdQuery, IList<ClassScheduleForTeacherDTO>>
{
    private readonly ILogger<GetClassScheduleByTeacherIdQueryHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetClassScheduleByTeacherIdQueryHandler(
        ILogger<GetClassScheduleByTeacherIdQueryHandler> logger,
        IApplicationDbContext context,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<IList<ClassScheduleForTeacherDTO>> Handle(GetClassScheduleByTeacherIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Search Teacher {} class schedule for year {}", request.TeacherId, request.Year);

        return await _context.ClassSchedules
            .Where(x => x.Teacher.Id == request.TeacherId && x.ClassRoom.Year == request.Year)
            .ProjectTo<ClassScheduleForTeacherDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}
