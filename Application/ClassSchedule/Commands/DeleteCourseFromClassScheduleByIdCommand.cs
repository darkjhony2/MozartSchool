using ColegioMozart.Application.Common.Exceptions;

namespace ColegioMozart.Application.ClassSchedule.Commands;

public class DeleteCourseFromClassScheduleByIdCommand : IRequest
{
    public Guid ClassScheduleId { get; set; }
}


public class DeleteCourseFromClassScheduleByIdCommandHandler : IRequestHandler<DeleteCourseFromClassScheduleByIdCommand>
{
    private readonly ILogger<DeleteCourseFromClassScheduleByIdCommandHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public DeleteCourseFromClassScheduleByIdCommandHandler(
        ILogger<DeleteCourseFromClassScheduleByIdCommandHandler> logger,
        IApplicationDbContext context,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(DeleteCourseFromClassScheduleByIdCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Eliminar un curso de un horario de clases : {}", request.ClassScheduleId);

        var entity = await _context.ClassSchedules.Where(x => x.Id == request.ClassScheduleId).FirstOrDefaultAsync();

        if (entity == null)
        {
            throw new NotFoundException("Horario de clases", request.ClassScheduleId);
        }

        _context.ClassSchedules.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}