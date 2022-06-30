using ColegioMozart.Application.ClassRoom.dtos;
using ColegioMozart.Application.Common.Security;

namespace ColegioMozart.Application.ClassRoom.Queries;

[Authorize]
public class GetAllClassRoomsByYear : IRequest<List<ClassRoomDto>>
{
    public int Year { get; set; }
}

public class GetAllClassRoomsByYearsHandler : IRequestHandler<GetAllClassRoomsByYear, List<ClassRoomDto>>
{
    private readonly IApplicationDbContext applicationDbContext;
    private readonly IMapper mapper;

    public GetAllClassRoomsByYearsHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
    {
        this.applicationDbContext = applicationDbContext;
        this.mapper = mapper;
    }

    public async Task<List<ClassRoomDto>> Handle(GetAllClassRoomsByYear request, CancellationToken cancellationToken)
    {
        return await applicationDbContext.ClassRooms
            .AsNoTracking()
            .Where(x => x.Year == request.Year)
            .Include(x => x.Tutor).ThenInclude( x => x.Person)
            .Include(x => x.Shift)
            .Include(x => x.Level)
            .Include(x => x.Section)
            .ProjectTo<ClassRoomDto>(mapper.ConfigurationProvider)
            .ToListAsync();
    }
}
