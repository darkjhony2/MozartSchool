using ColegioMozart.Application.Common.Mappings;
using ColegioMozart.Application.Common.Models;

namespace ColegioMozart.Application.Subjects.Queries.GetSubjectsByFilters;

[Authorize]
public class GetSubjectsByFiltersQuery : IRequest<PaginatedList<ESubjectDTO>>
{
    public SubjectFilterDTO SubjectFilter { get; set; }
    public PageModelFilterDTO Page { get; set; }
}

public class GetSubjectsByFiltersQueryHandler : IRequestHandler<GetSubjectsByFiltersQuery, PaginatedList<ESubjectDTO>>
{
    private readonly ILogger<GetSubjectsByFiltersQueryHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetSubjectsByFiltersQueryHandler(
        ILogger<GetSubjectsByFiltersQueryHandler> logger,
        IApplicationDbContext context,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<PaginatedList<ESubjectDTO>> Handle(GetSubjectsByFiltersQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Se filtra las materias @{request}", request);

        return await _context
             .Subjects
             .Where(x => EF.Functions.Like(x.Name, "%" + request.SubjectFilter.Name + "%"))
             .OrderBy(x => x.Name)
             .ProjectTo<ESubjectDTO>(_mapper.ConfigurationProvider)
             .PaginatedListAsync(request.Page.PageNumber, request.Page.PageSize);
    }
}
