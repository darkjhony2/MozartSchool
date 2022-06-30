using ColegioMozart.Application.AttendanceStatus.Dtos;
using ColegioMozart.Application.Common.Exceptions;
using ColegioMozart.Application.Common.Security;

namespace ColegioMozart.Application.AttendanceStatus.Queries;


[Authorize]
public class GetAttendanceStatusByIdQuery : IRequest<AttendanceStatusDTO>
{
    public int Id { get; set; }
}

public class GetAttendanceStatusByIdQueryHandler : IRequestHandler<GetAttendanceStatusByIdQuery, AttendanceStatusDTO>
{
    private readonly ILogger<GetAttendanceStatusByIdQueryHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetAttendanceStatusByIdQueryHandler(
        ILogger<GetAttendanceStatusByIdQueryHandler> logger,
        IApplicationDbContext context,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<AttendanceStatusDTO> Handle(GetAttendanceStatusByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Buscar tipo de asitencia por id {}", request.Id);

        var entity = await _context.AttendanceStatus
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .ProjectTo<AttendanceStatusDTO>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException("Tipo de asistencia", request.Id);
        }

        return entity;
    }
}

