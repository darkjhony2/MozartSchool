using ColegioMozart.Application.Common.Exceptions;

namespace ColegioMozart.Application.Sections.Commands;

public class DeleteSectionCommand : IRequest
{
    public int Id { get; set; }
}

public class DeleteSectionCommandHandler : IRequestHandler<DeleteSectionCommand>
{
    private readonly ILogger<DeleteSectionCommandHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public DeleteSectionCommandHandler(
        ILogger<DeleteSectionCommandHandler> logger,
        IApplicationDbContext context,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(DeleteSectionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Delete section by Id : {Id}", request.Id);

        var entity = await _context.Sections.Where(x => x.Id == request.Id).FirstOrDefaultAsync();

        if (entity == null)
        {
            throw new NotFoundException("Sección", request.Id);
        }

        _context.Sections.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}