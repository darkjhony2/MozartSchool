using ColegioMozart.Application.AcademicScale.Dtos;
using ColegioMozart.Application.Common.Exceptions;

namespace ColegioMozart.Application.AcademicScale.Queries;

public class GetAcademicScaleByIdQuery : IRequest<AcademicScaleDTO>
{
    public int Id { get; set; } 
}


public class GetAcademicScaleByIdQueryHandler : IRequestHandler<GetAcademicScaleByIdQuery, AcademicScaleDTO>
{
    private readonly ILogger<GetAcademicScaleByIdQueryHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetAcademicScaleByIdQueryHandler(
        ILogger<GetAcademicScaleByIdQueryHandler> logger,
        IApplicationDbContext context,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<AcademicScaleDTO> Handle(GetAcademicScaleByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Se busca escala academica por id : {}", request.Id);

        var response =  await _context.AcademicScales
            .ProjectTo<AcademicScaleDTO>(_mapper.ConfigurationProvider)
            .OrderBy(x => x.Name)
            .FirstOrDefaultAsync();


        if(response == null)
        {
            throw new NotFoundException("Escala académica", request.Id);
        }

        return response;
    }
}

