using ColegioMozart.Application.ClassRoom.dtos;
using ColegioMozart.Application.Common.Exceptions;
using ColegioMozart.Application.Common.Security;

namespace ColegioMozart.Application.ClassRoom.Queries;


[Authorize]
public class GetClassroomByStudentIdQuery : IRequest<ClassRoomDto>
{
    public Guid StudentId { get; set; }
    public int Year { get; set; }
}


public class GetClassroomByStudentIdQueryHandler : IRequestHandler<GetClassroomByStudentIdQuery, ClassRoomDto>
{
    private readonly ILogger<GetClassroomByStudentIdQueryHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetClassroomByStudentIdQueryHandler(
        ILogger<GetClassroomByStudentIdQueryHandler> logger,
        IApplicationDbContext context,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<ClassRoomDto> Handle(GetClassroomByStudentIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Find student classroom by studentId {} for year {}", request.StudentId, request.Year);

        var classroom = await _context
            .ClassRooms
            .Where(x => x.Students.Where(s => s.Id == request.StudentId).Any() && x.Year == request.Year)
            .ProjectTo<ClassRoomDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (classroom == null)
        {
            throw new NotFoundException($"No se encontró el salón asociado al alumno con código {request.StudentId}");
        }

        return classroom;
    }
}