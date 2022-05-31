using ColegioMozart.Application.Common.Exceptions;

namespace ColegioMozart.Application.AcademicScale.Commands;

public class DeleteAcademicLevelCommand : IRequest
{
    public int Id { get; set; }
}


public class DeleteAcademicLevelCommandHandler : IRequestHandler<DeleteAcademicLevelCommand>
{
    private readonly ILogger<DeleteAcademicLevelCommandHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public DeleteAcademicLevelCommandHandler(
        ILogger<DeleteAcademicLevelCommandHandler> logger,
        IApplicationDbContext context,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(DeleteAcademicLevelCommand request, CancellationToken cancellationToken)
    {
        var academicLevel = await _context
            .AcademicLevels
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync();

        if (academicLevel == null)
        {
            throw new NotFoundException("Nivel académico", request.Id);
        }

        _context.AcademicLevels.Remove(academicLevel);

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Eliminar nivel académico : {}", request.Id);

        return Unit.Value;
    }
}