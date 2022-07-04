using ColegioMozart.Application.Common.Exceptions;
using ColegioMozart.Application.Common.Mappings;
using ColegioMozart.Domain.Entities;

namespace ColegioMozart.Application.Teachers.Commands;

[Authorize(Roles = "Administrator, Director")]
public class CreateTeacherCommand : IRequest, IMapTo<ETeacher>
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string MothersLastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int DocumentTypeId { get; set; }
    public string DocumentNumber { get; set; }
    public int GenderId { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateTeacherCommand, ETeacher>()
            .ForPath(d => d.Person.Name, opt => opt.MapFrom(s => s.Name))
            .ForPath(d => d.Person.LastName, opt => opt.MapFrom(s => s.LastName))
            .ForPath(d => d.Person.MothersLastName, opt => opt.MapFrom(s => s.MothersLastName))
            .ForPath(d => d.Person.DateOfBirth, opt => opt.MapFrom(s => s.DateOfBirth))
            .ForPath(d => d.Person.DocumentTypeId, opt => opt.MapFrom(s => s.DocumentTypeId))
            .ForPath(d => d.Person.DocumentNumber, opt => opt.MapFrom(s => s.DocumentNumber))
            .ForPath(d => d.Person.GenderId, opt => opt.MapFrom(s => s.GenderId))
            .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
            .ForMember(d => d.Phone, opt => opt.MapFrom(s => s.Phone));
    }
}

public class CreateTeacherCommandHandler : IRequestHandler<CreateTeacherCommand>
{
    private readonly ILogger<CreateTeacherCommandHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;

    public CreateTeacherCommandHandler(
        ILogger<CreateTeacherCommandHandler> logger,
        IApplicationDbContext context,
        IMapper mapper,
        IIdentityService identityService)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
        _identityService = identityService;
    }

    public async Task<Unit> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
    {
        if (!await _context.DocumentTypes.AnyAsync(x => x.Id == request.DocumentTypeId))
        {
            throw new NotFoundException(nameof(EDocumentType), request.DocumentTypeId);
        }

        if (!await _context.Genders.AnyAsync(x => x.Id == request.GenderId))
        {
            throw new NotFoundException(nameof(EGender), request.GenderId);
        }

        if (await _context
            .Persons
            .Where(x => x.DocumentTypeId == request.DocumentTypeId && x.DocumentNumber == request.DocumentNumber)
            .AnyAsync())
        {
            throw new EntityAlreadyExistException(new HashSet<string> { "DocumentTypeId", "DocumentNumber" });
        }


        var teacher = _mapper.Map<ETeacher>(request);

        var userData = await _identityService.CreateUserAsync(request.DocumentNumber, $"{request.DocumentNumber}@Moz!", "Docente");

        if (userData.Result.Succeeded)
        {
            teacher.Person.UserId = userData.UserId;
        }

        await _context.Teachers.AddAsync(teacher, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return await Unit.Task;
    }
}
