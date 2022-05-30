using ColegioMozart.Application.DocumentTypes.Dtos;

namespace ColegioMozart.Application.DocumentTypes.Queries;

public class GetAllDocumentTypesQuery : IRequest<IList<DocumentTypeDTO>>
{
}

public class GetAllDocumentTypesQueryHandler : IRequestHandler<GetAllDocumentTypesQuery, IList<DocumentTypeDTO>>
{
    private readonly ILogger<GetAllDocumentTypesQueryHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllDocumentTypesQueryHandler(
        ILogger<GetAllDocumentTypesQueryHandler> logger,
        IApplicationDbContext context,
        IMapper mapper)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
    }

    public async Task<IList<DocumentTypeDTO>> Handle(GetAllDocumentTypesQuery request, CancellationToken cancellationToken)
    {
        return await _context.DocumentTypes
            .AsNoTracking()
            .ProjectTo<DocumentTypeDTO>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
