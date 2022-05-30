namespace ColegioMozart.Application.Teachers.Queries;

public class GetAllTeachersQuery : IRequest<IList<TeacherDTO>>
{

}

public class GetAllTeachersQueryHandler : IRequestHandler<GetAllTeachersQuery, IList<TeacherDTO>>
{
    private readonly ILogger<GetAllTeachersQueryHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllTeachersQueryHandler(
        ILogger<GetAllTeachersQueryHandler> logger,
        IApplicationDbContext context,
        IMapper mapper
    )
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
    }

    public async Task<IList<TeacherDTO>> Handle(GetAllTeachersQuery request, CancellationToken cancellationToken)
    {
        return await _context.Teachers
           .AsNoTracking()
           .ProjectTo<TeacherDTO>(_mapper.ConfigurationProvider)
           .ToListAsync(cancellationToken);
    }
}
