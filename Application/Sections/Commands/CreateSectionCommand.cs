using ColegioMozart.Application.Common.Exceptions;
using ColegioMozart.Application.Common.Security;
using ColegioMozart.Domain.Entities;

namespace ColegioMozart.Application.Sections.Commands;


[Authorize]
public class CreateSectionCommand : IRequest
{
    public string Name { get; set; }
}

public class CreateSectionCommandHandler : IRequestHandler<CreateSectionCommand>
{
    private readonly ILogger<CreateSectionCommandHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public CreateSectionCommandHandler(
        ILogger<CreateSectionCommandHandler> logger,
        IApplicationDbContext context,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(CreateSectionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating a new section");

        if (_context.Sections.Where(x => x.Name == request.Name.Trim()).Any())
        {
            throw new EntityAlreadyExistException("Name");
        }

        var entity = new ESection
        {
            Name = request.Name.Trim()
        };

        _context.Sections.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Created a new section with ID {}", entity.Id);

        return Unit.Value;
    }
}
