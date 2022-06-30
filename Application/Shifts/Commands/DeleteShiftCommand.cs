using ColegioMozart.Application.Common.Exceptions;

namespace ColegioMozart.Application.Shifts.Commands;

[Authorize]
public class DeleteShiftCommand : IRequest
{
    public int Id { get; set; }
}


public class DeleteShiftCommandHandler : IRequestHandler<DeleteShiftCommand>
{
    private readonly ILogger<DeleteShiftCommandHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public DeleteShiftCommandHandler(
        ILogger<DeleteShiftCommandHandler> logger,
        IApplicationDbContext context,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(DeleteShiftCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Se eliminará un turno : {}", request.Id);

        var entity = await _context.Shifts.Where(x => x.Id == request.Id).FirstOrDefaultAsync();

        if (entity == null)
        {
            throw new NotFoundException("Turno", request.Id);
        }

        _context.Shifts.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}