using ColegioMozart.Application.Common.Exceptions;

namespace ColegioMozart.Application.Shifts.Commands;

[Authorize]
public class UpdateShiftCommand : IRequest
{
    public int Id { get; set; }
    public UpdateShiftResource Resource { get; set; }
}

public class UpdateShiftCommandHandler : IRequestHandler<UpdateShiftCommand>
{
    private readonly ILogger<UpdateShiftCommandHandler> _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateShiftCommandHandler(
        ILogger<UpdateShiftCommandHandler> logger,
        ICurrentUserService currentUserService,
        IApplicationDbContext context,
        IMapper mapper)
    {
        _logger = logger;
        _currentUserService = currentUserService;
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateShiftCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Shifts.Where(x => x.Id == request.Id).FirstOrDefaultAsync();

        if (entity == null)
        {
            throw new NotFoundException("Turno", request.Id);
        }

        entity.Name = request.Resource.Name;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}