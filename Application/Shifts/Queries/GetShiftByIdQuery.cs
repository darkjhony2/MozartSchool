using ColegioMozart.Application.Common.Exceptions;

namespace ColegioMozart.Application.Shifts.Queries;

public class GetShiftByIdQuery : IRequest<ShiftDTO>
{
    public int Id { get; set; }
}

public class GetShiftByIdQueryHandler : IRequestHandler<GetShiftByIdQuery, ShiftDTO>
{
    private readonly ILogger<GetShiftByIdQueryHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetShiftByIdQueryHandler(
        ILogger<GetShiftByIdQueryHandler> logger,
        IApplicationDbContext context,
        IMapper mapper)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
    }

    public async Task<ShiftDTO> Handle(GetShiftByIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.Shifts
           .AsNoTracking()
           .ProjectTo<ShiftDTO>(_mapper.ConfigurationProvider)
           .FirstOrDefaultAsync(cancellationToken);

        if (data == null)
        {
            throw new NotFoundException();
        }

        return data;
    }
}
