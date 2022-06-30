using ColegioMozart.Application.Common.Exceptions;
using ColegioMozart.Application.Common.Security;
using ColegioMozart.Domain.Entities;

namespace ColegioMozart.Application.ClassRoom.Commands;


[Authorize]
public class CreateClassroomCommand : IRequest
{
    public int AcademicLevelId { get; set; }
    public int ShiftId { get; set; }
    public Guid TutorId { get; set; }
    public int SectionId { get; set; }

}


public class CreateClassroomCommandHandler : IRequestHandler<CreateClassroomCommand>
{
    private readonly ILogger<CreateClassroomCommandHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public CreateClassroomCommandHandler(
        ILogger<CreateClassroomCommandHandler> logger,
        IApplicationDbContext context,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(CreateClassroomCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creando nuevo salón de clase");
        int currentYear = DateTime.Now.Year;

        if (!await _context.AcademicLevels.Where(x => x.Id == request.AcademicLevelId).AnyAsync())
        {
            throw new NotFoundException("Grado", request.AcademicLevelId);
        }

        if (!await _context.Shifts.Where(x => x.Id == request.ShiftId).AnyAsync())
        {
            throw new NotFoundException("Turno", request.ShiftId);
        }

        if (!await _context.Teachers.Where(x => x.Id == request.TutorId).AnyAsync())
        {
            throw new NotFoundException("Tutor", request.TutorId);
        }

        if (!await _context.Sections.Where(x => x.Id == request.SectionId).AnyAsync())
        {
            throw new NotFoundException("Sección", request.SectionId);
        }


        if (await _context
            .ClassRooms
            .Where(x => x.Year == currentYear
                    && x.LevelId == request.AcademicLevelId
                    && x.ShiftId == request.ShiftId
                    && x.SectionId == request.SectionId)
            .AnyAsync())
        {
            throw new EntityAlreadyExistException("Ya existe un salón de clase con los mismos datos");
        }


        if (await _context
            .ClassRooms
            .Where(x => x.Year == currentYear
                && x.TutorId == request.TutorId
                && x.ShiftId == request.ShiftId)
            .AnyAsync())
        {
            var teacher = await _context.Teachers
                .Where(x => x.Id == request.TutorId)
                .Include(x => x.Person)
                .FirstAsync();

            throw new EntityAlreadyExistException($"No se puede registrar debido a qué el docente {teacher.Person.Name} {teacher.Person.LastName} {teacher.Person.MothersLastName} ya es tutor de otro salón.");
        }
        

        var classroom = new EClassRoom
        {
            Year = currentYear,
            LevelId = request.AcademicLevelId,
            ShiftId = request.ShiftId,
            TutorId = request.TutorId,
            SectionId = request.SectionId
        };

        _logger.LogInformation("Se agregará el salón de clases @{classroom}", classroom);

        _context.ClassRooms.Add(classroom);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}