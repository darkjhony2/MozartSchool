using ColegioMozart.Application.Common.Exceptions;

namespace ColegioMozart.Application.StudentClassroom.Queries.StudentsByClassroom;

public class GetAllStudentsByClassroomId : IRequest<StudentClassroomDTO>
{
    public Guid ClassroomId { get; set; }
}

public class GetAllStudentsByClassroomIdHandler : IRequestHandler<GetAllStudentsByClassroomId, StudentClassroomDTO>
{
    private readonly ILogger<GetAllStudentsByClassroomIdHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetAllStudentsByClassroomIdHandler(
        ILogger<GetAllStudentsByClassroomIdHandler> logger,
        IApplicationDbContext context,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<StudentClassroomDTO> Handle(GetAllStudentsByClassroomId request, CancellationToken cancellationToken)
    {
        var data = await _context.ClassRooms
               .AsNoTracking()
               .Where(x => x.Id == request.ClassroomId)
               .Include(x => x.Tutor).ThenInclude(x => x.Person)
               .Include(x => x.Shift)
               .Include(x => x.Level)
               .Include(x => x.Section)
               .Include(x => x.Students).ThenInclude(x => x.Person)
               .ProjectTo<StudentClassroomDTO>(_mapper.ConfigurationProvider)
               .FirstOrDefaultAsync();

        if (data == null)
        {
            throw new NotFoundException("Salón de clases", request.ClassroomId);
        }


        return data;
    }
}