using ColegioMozart.Application.Common.Exceptions;
using ColegioMozart.Domain.Entities;

namespace ColegioMozart.Application.Students.Commands;

public class CreateStudentCommand : IRequest
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string MothersLastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int DocumentTypeId { get; set; }
    public string DocumentNumber { get; set; }
    public int GenderId { get; set; }
    public int CurrentAcademicLevelId { get; set; }
}

public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand>
{
    private readonly ILogger<CreateStudentCommandHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public CreateStudentCommandHandler(
        ILogger<CreateStudentCommandHandler> logger,
        IApplicationDbContext context,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creando estudiante");

        if (!await _context.DocumentTypes.AnyAsync(x => x.Id == request.DocumentTypeId))
        {
            throw new NotFoundException("Tipo de documento", request.DocumentTypeId);
        }

        if (!await _context.Genders.AnyAsync(x => x.Id == request.GenderId))
        {
            throw new NotFoundException("Géneros", request.GenderId);
        }

        if (!await _context.AcademicLevels.AnyAsync(x => x.Id == request.CurrentAcademicLevelId))
        {
            throw new NotFoundException("Grado", request.CurrentAcademicLevelId);
        }

        if (await _context
            .Persons
            .Where(x => x.DocumentTypeId == request.DocumentTypeId && x.DocumentNumber == request.DocumentNumber)
            .AnyAsync())
        {
            throw new EntityAlreadyExistException(new HashSet<string> { "DocumentTypeId", "DocumentNumber" });
        }


        var student = new EStudent()
        {
            Person = new EPerson()
            {
                Name = request.Name,
                MothersLastName = request.MothersLastName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                DocumentTypeId = request.DocumentTypeId,
                DocumentNumber = request.DocumentNumber,
                GenderId = request.GenderId
            },
            CurrentAcademicLevelId = request.CurrentAcademicLevelId
        };

        _logger.LogInformation("Se creará el estudiante @{student}", student);

        _context.Students.Add(student);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
