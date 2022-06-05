using ColegioMozart.Application.AttendanceStatus.Dtos;

namespace ColegioMozart.Application.AttendanceStatus.Queries;

public class GetAllAttendanceStatusQuery : IRequest<IList<AttendanceStatusDTO>>
{

}

public class GetAllAttendanceStatusQueryHandler : IRequestHandler<GetAllAttendanceStatusQuery, IList<AttendanceStatusDTO>>
{
    private readonly ILogger<GetAllAttendanceStatusQueryHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetAllAttendanceStatusQueryHandler(
        ILogger<GetAllAttendanceStatusQueryHandler> logger,
        IApplicationDbContext context,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<IList<AttendanceStatusDTO>> Handle(GetAllAttendanceStatusQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Listar los tipos de asistencia");

        return await _context
            .AttendanceStatus
            .ProjectTo<AttendanceStatusDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}