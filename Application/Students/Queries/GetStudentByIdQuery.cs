using ColegioMozart.Application.Common.Exceptions;
using ColegioMozart.Application.Students.Dtos;

namespace ColegioMozart.Application.Students.Queries;

[Authorize]
public class GetStudentByIdQuery : IRequest<StudentDetailDTO>
{
    public Guid StudentId { get; set; }
}

public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, StudentDetailDTO>
{
    private readonly ILogger<GetStudentByIdQueryHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetStudentByIdQueryHandler(
        ILogger<GetStudentByIdQueryHandler> logger,
        IApplicationDbContext context,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<StudentDetailDTO> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Buscar el detalle de un alumno por su Id {}", request.StudentId);

        var entity = await _context.Students.Where(x => x.Id == request.StudentId)
            .ProjectTo<StudentDetailDTO>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        if (entity == null)
        {
            throw new NotFoundException("Estudiante", request.StudentId);
        }

        return entity;
    }
}
