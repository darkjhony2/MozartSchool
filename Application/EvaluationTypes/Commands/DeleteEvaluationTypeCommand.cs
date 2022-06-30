using ColegioMozart.Application.Common.Security;
using ColegioMozart.Application.EvaluationTypes.Queries;
using ColegioMozart.Domain.Entities;

namespace ColegioMozart.Application.EvaluationTypes.Commands;

[Authorize]
public class DeleteEvaluationTypeCommand : IRequest
{
    public int Id { get; set; }
}


public class DeleteEvaluationTypeCommandHandler : IRequestHandler<DeleteEvaluationTypeCommand>
{

    private readonly ILogger<DeleteEvaluationTypeCommandHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;
    private readonly ISender _mediator;

    public DeleteEvaluationTypeCommandHandler(
        ILogger<DeleteEvaluationTypeCommandHandler> logger,
        IApplicationDbContext context,
        IMapper mapper,
        ICurrentUserService currentUserService,
        ISender mediator)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(DeleteEvaluationTypeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Eliminar tipo de evaluación {}", request.Id);

        var found = await _mediator.Send(new GetEvaluationTypeByIdQuery { Id = request.Id });

        _context.EvaluationTypes.Remove(new EEvaluationType { Id = found.Id });

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
