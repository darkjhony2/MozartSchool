using ColegioMozart.Application.Common.Exceptions;
using ColegioMozart.Domain.Entities;

namespace ColegioMozart.Application.Sections.Queries;

public class GetSectionByIdQuery : IRequest<SectionDTO>
{
    public int Id { get; set; }
}

public class GetSectionByIdQueryHandler : IRequestHandler<GetSectionByIdQuery, SectionDTO>
{
    private readonly ILogger<GetSectionByIdQueryHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetSectionByIdQueryHandler(
        ILogger<GetSectionByIdQueryHandler> logger,
        IApplicationDbContext context,
        IMapper mapper)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
    }

    public async Task<SectionDTO> Handle(GetSectionByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Sections
            .ProjectTo<SectionDTO>(_mapper.ConfigurationProvider)
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync();

        if (entity == null)
        {
            throw new NotFoundException(nameof(ESection), request.Id);
        }

        return entity;
    }
}
