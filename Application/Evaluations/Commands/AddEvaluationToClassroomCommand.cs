using ColegioMozart.Application.Evaluations.Dtos;

namespace ColegioMozart.Application.Evaluations.Commands;

public class AddEvaluationToClassroomCommand : IRequest
{
    public AddEvaluationResource Resource { get; set; }
}

public class AddEvaluationToClassroomCommandHandler : IRequestHandler<AddEvaluationToClassroomCommand>
{
    private readonly ILogger<AddEvaluationToClassroomCommandHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;
    private readonly ISender _mediator;

    public AddEvaluationToClassroomCommandHandler(
        ILogger<AddEvaluationToClassroomCommandHandler> logger,
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

    public Task<Unit> Handle(AddEvaluationToClassroomCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Crear nueva evaluación");


        


        throw new NotImplementedException();
    }
}
