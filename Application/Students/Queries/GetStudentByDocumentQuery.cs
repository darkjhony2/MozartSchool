using ColegioMozart.Application.Common.Exceptions;
using ColegioMozart.Application.Students.Dtos;

namespace ColegioMozart.Application.Students.Queries;

[Authorize]
public class GetStudentByDocumentQuery : IRequest<StudentDetailDTO>
{
    public int DocumentTypeId { get; set; }
    public string DocumentNumber { get; set; }
}

public class GetStudentByDocumentQueryHandler : IRequestHandler<GetStudentByDocumentQuery, StudentDetailDTO>
{
    private readonly ILogger<GetStudentByDocumentQueryHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetStudentByDocumentQueryHandler(
        ILogger<GetStudentByDocumentQueryHandler> logger,
        IApplicationDbContext context,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<StudentDetailDTO> Handle(GetStudentByDocumentQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Buscar el detalle de un alumno por documento @{document}", request);

        var entity = await _context
            .Students
            .Where(x => x.Person.DocumentNumber == request.DocumentNumber && x.Person.DocumentTypeId == request.DocumentTypeId)
            .ProjectTo<StudentDetailDTO>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            var docType = await _context
                .DocumentTypes.Where(x => x.Id == request.DocumentTypeId)
                .Select(x => x.Name)
                .FirstAsync();

            throw new NotFoundException("Estudiante", $"{docType} - {request.DocumentNumber}");
        }

        return entity;
    }
}
