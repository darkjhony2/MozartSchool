namespace ColegioMozart.Application.Sections.Queries;

[Authorize]
public class GetSectionsQuery : IRequest<IList<SectionDTO>>
{
    
}

public class GetSectionsQueryHandler : IRequestHandler<GetSectionsQuery, IList<SectionDTO>>
{
    private readonly ILogger<GetSectionsQueryHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetSectionsQueryHandler(
        ILogger<GetSectionsQueryHandler> logger,
        IApplicationDbContext context,
        IMapper mapper)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
    }

    public async Task<IList<SectionDTO>> Handle(GetSectionsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Sections
           .AsNoTracking()
           .ProjectTo<SectionDTO>(_mapper.ConfigurationProvider)
           .ToListAsync(cancellationToken);
    }
}