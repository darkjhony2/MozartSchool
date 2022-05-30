using ColegioMozart.Application.Students.Dtos;

namespace ColegioMozart.Application.Students.Queries;

public class GetAllStudentsQuery : IRequest<IList<StudentDTO>>
{
}

public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, IList<StudentDTO>>
{
    private readonly ILogger<GetAllStudentsQueryHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllStudentsQueryHandler(
        ILogger<GetAllStudentsQueryHandler> logger,
        IApplicationDbContext context,
        IMapper mapper)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
    }

    public async Task<IList<StudentDTO>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Students
             .AsNoTracking()
             .ProjectTo<StudentDTO>(_mapper.ConfigurationProvider)
             .ToListAsync(cancellationToken);
    }
}
