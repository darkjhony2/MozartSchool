using ColegioMozart.Application.Common.Exceptions;
using ColegioMozart.Application.Common.Security;

namespace ColegioMozart.Application.AcademicPeriods.Commands;


[Authorize]
public class DeleteAcademicPeriodCommand : IRequest
{
    public Guid Id { get; set; }
}

public class DeleteAcademicPeriodCommandHandler : IRequestHandler<DeleteAcademicPeriodCommand>
{
    private readonly ILogger<DeleteAcademicPeriodCommandHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;
    public DeleteAcademicPeriodCommandHandler(
        ILogger<DeleteAcademicPeriodCommandHandler> logger,
        IApplicationDbContext context,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(DeleteAcademicPeriodCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Se eliminará periodo academico");

        var academicPeriod = await _context.AcademicPeriods.Where(x => x.Id == request.Id).FirstOrDefaultAsync();

        if (academicPeriod == null)
        {
            throw new NotFoundException("Periodo académico", request.Id);
        }

        _context.AcademicPeriods.Remove(academicPeriod);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}