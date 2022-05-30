using ColegioMozart.Application.ClassSchedule.Dtos;

namespace ColegioMozart.Application.ClassSchedule.Queries;

public class GetClassScheduleByClassroomId : IRequest<IList<ClassScheduleDTO>>
{
    public Guid ClassroomId { get; set; }
}

public class GetClassScheduleByClassroomIdHandler : IRequestHandler<GetClassScheduleByClassroomId, IList<ClassScheduleDTO>>
{
    private readonly ILogger<GetClassScheduleByClassroomIdHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetClassScheduleByClassroomIdHandler(
        ILogger<GetClassScheduleByClassroomIdHandler> logger,
        IApplicationDbContext context,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<IList<ClassScheduleDTO>> Handle(GetClassScheduleByClassroomId request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Se solicita la información de la clase por id de aula {ClassroomId}", request.ClassroomId);

        return await _context.ClassSchedules
            .Where(x => x.ClassRoomId == request.ClassroomId)
            .Include(x => x.Subject)
            .Include(x => x.Teacher)
            .ProjectTo<ClassScheduleDTO>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
