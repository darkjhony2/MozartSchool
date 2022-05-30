using ColegioMozart.Application.Students.Dtos;

namespace ColegioMozart.Application.Students.Queries;

public class GetStudentsByAcademicLevelQuery : IRequest<IList<StudentDTO>>
{
    public int AcademicLevelId { get; set; }
}

public class GetStudentsByAcademicLevelQueryHandler : IRequestHandler<GetStudentsByAcademicLevelQuery, IList<StudentDTO>>
{
    private readonly ILogger<GetStudentsByAcademicLevelQueryHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetStudentsByAcademicLevelQueryHandler(
        ILogger<GetStudentsByAcademicLevelQueryHandler> logger, 
        IApplicationDbContext context, 
        IMapper mapper)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
    }


    public async Task<IList<StudentDTO>> Handle(GetStudentsByAcademicLevelQuery request, CancellationToken cancellationToken)
    {
        return await _context.Students
         .AsNoTracking()
         .Where(x => x.CurrentAcademicLevelId == request.AcademicLevelId)
         .ProjectTo<StudentDTO>(_mapper.ConfigurationProvider)
         .ToListAsync(cancellationToken);
    }
}
