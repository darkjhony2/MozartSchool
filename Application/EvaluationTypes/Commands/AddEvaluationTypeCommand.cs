using ColegioMozart.Application.Common.Exceptions;
using ColegioMozart.Domain.Entities;

namespace ColegioMozart.Application.EvaluationTypes.Commands;

public class AddEvaluationTypeCommand : IRequest
{
    public string Name { get; set; }
    public string? Description { get; set; }
}

public class AddEvaluationTypeCommandHandler : IRequestHandler<AddEvaluationTypeCommand>
{
    private readonly ILogger<AddEvaluationTypeCommandHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public AddEvaluationTypeCommandHandler(
        ILogger<AddEvaluationTypeCommandHandler> logger,
        IApplicationDbContext context,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(AddEvaluationTypeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Registrar nuevo tipo de evaluación : @{request}", request);


        if (await _context.EvaluationTypes
            .Where(x => x.Name == request.Name)
            .AnyAsync())
        {
            throw new EntityAlreadyExistException();
        }

        _context.EvaluationTypes.Add(new EEvaluationType
        {
            Name = request.Name,
            Description = request.Description
        });

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
