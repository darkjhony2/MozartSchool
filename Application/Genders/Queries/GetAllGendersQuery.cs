using ColegioMozart.Application.Genders.Dtos;

namespace ColegioMozart.Application.Genders.Queries;

public class GetAllGendersQuery : IRequest<IList<GenderDTO>>
{
    
}

public class GetAllGendersQueryHandler : IRequestHandler<GetAllGendersQuery, IList<GenderDTO>>
{
    private readonly ILogger<GetAllGendersQueryHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllGendersQueryHandler(
        ILogger<GetAllGendersQueryHandler> logger,
        IApplicationDbContext context,
        IMapper mapper)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
    }

    public async Task<IList<GenderDTO>> Handle(GetAllGendersQuery request, CancellationToken cancellationToken)
    {
        return await _context.Genders
              .AsNoTracking()
              .ProjectTo<GenderDTO>(_mapper.ConfigurationProvider)
              .ToListAsync(cancellationToken);
    }
}
