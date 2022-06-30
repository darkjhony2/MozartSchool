using ColegioMozart.Application.Common.Exceptions;

namespace ColegioMozart.Application.Students.Commands;

[Authorize]
public class AssociateUserToClassroomCommand : IRequest
{
    public Guid ClassroomId { get; set; }
    public Guid StudentId { get; set; }
}

public class AssociateUserToClassroomCommandHandler : IRequestHandler<AssociateUserToClassroomCommand>
{
    private readonly ILogger<AssociateUserToClassroomCommandHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public AssociateUserToClassroomCommandHandler(
        ILogger<AssociateUserToClassroomCommandHandler> logger,
        IApplicationDbContext context,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(AssociateUserToClassroomCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Associating student {} to classroom {}", request.StudentId, request.ClassroomId);

        var classroom = await _context
            .ClassRooms
            .Include(x=> x.Shift)
            .Include(x=> x.Level)
            .Include(x=> x.Section)
            .Where(x => x.Id == request.ClassroomId)
            .FirstOrDefaultAsync(cancellationToken);

        if (classroom == null)
        {
            throw new NotFoundException("Salón de clases", request.ClassroomId);
        }

        var student = await _context
            .Students
            .Include(x => x.Person)
            .Where(x => x.Id == request.StudentId)
            .FirstOrDefaultAsync();

        if (student == null)
        {
            throw new NotFoundException("Estudiante", request.StudentId);
        }

        if (student.ClassRoomId != null)
        {
            throw new AlreadyAssociatedException(
                $"Estudiante({student.Person.Name} {student.Person.LastName}) ya asociado a un salón de " +
                $"clases ({classroom.Level.Level} {classroom.Shift.Name} Sección {classroom.Section.Name})");
        }

        student.ClassRoom = classroom;
        _logger.LogInformation("User {} associated to classroom {}", student.Id, classroom.Id);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
