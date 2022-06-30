using ColegioMozart.Application.ClassRoom.dtos;
using ColegioMozart.Application.Common.Exceptions;
using ColegioMozart.Application.Common.Security;

namespace ColegioMozart.Application.ClassRoom.Queries;

[Authorize]
public class GetClassroomByIdQuery : IRequest<ClassRoomDto>
{
    public Guid Id { get; set; }
}

public class GetClassroomByIdQueryHandler : IRequestHandler<GetClassroomByIdQuery, ClassRoomDto>
{
    private readonly ILogger<GetClassroomByIdQueryHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetClassroomByIdQueryHandler(
        ILogger<GetClassroomByIdQueryHandler> logger,
        IApplicationDbContext context,
        IMapper mapper)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
    }

    public async Task<ClassRoomDto> Handle(GetClassroomByIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.ClassRooms
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
                .Include(x => x.Tutor).ThenInclude(x => x.Person)
                .Include(x => x.Shift)
                .Include(x => x.Level)
                .Include(x => x.Section)
                .ProjectTo<ClassRoomDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

        if (data == null)
        {
            throw new NotFoundException("Salón de clase", request.Id);
        }

        return data;
    }
}
