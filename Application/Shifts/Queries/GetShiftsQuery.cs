namespace ColegioMozart.Application.Shifts.Queries;

public class GetShiftsQuery : IRequest<IList<ShiftDTO>>
{
}

public class GetShiftsQueryHandler : IRequestHandler<GetShiftsQuery, IList<ShiftDTO>>
{
    private readonly ILogger<GetShiftsQueryHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetShiftsQueryHandler(
        ILogger<GetShiftsQueryHandler> logger,
        IApplicationDbContext context,
        IMapper mapper)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
    }

    public async Task<IList<ShiftDTO>> Handle(GetShiftsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Shifts
          .AsNoTracking()
          .ProjectTo<ShiftDTO>(_mapper.ConfigurationProvider)
          .ToListAsync(cancellationToken);
    }
}
