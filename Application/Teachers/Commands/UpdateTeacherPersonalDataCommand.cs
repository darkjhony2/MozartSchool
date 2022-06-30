using ColegioMozart.Application.Common.Exceptions;
using ColegioMozart.Application.Teachers.Dtos;
using ColegioMozart.Domain.Entities;

namespace ColegioMozart.Application.Teachers.Commands;

[Authorize]
public class UpdateTeacherPersonalDataCommand : IRequest
{
    public Guid TeacherId { get; set; }
    public UpdateTeacherResource Resource { get; set; }
}

public class UpdateTeacherPersonalDataCommandHandler : IRequestHandler<UpdateTeacherPersonalDataCommand>
{
    private readonly ILogger<UpdateTeacherPersonalDataCommandHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public UpdateTeacherPersonalDataCommandHandler(
        ILogger<UpdateTeacherPersonalDataCommandHandler> logger,
        IApplicationDbContext context,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(UpdateTeacherPersonalDataCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Update teacher personal data @{resource} with Id {}", request.Resource, request.TeacherId);

        var entity = await _context.Teachers.Where(x => x.Id == request.TeacherId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (entity == null)
        {
            throw new NotFoundException("Docente", request.TeacherId);
        }

        var dto = _mapper.Map<ETeacher>(request.Resource);
        dto.Id = entity.Id;
        dto.PersonId = dto.Person.Id = entity.PersonId;

        _context.Teachers.Attach(dto);
        _context.Persons.Attach(dto.Person);

        _context.Entry(dto).State = EntityState.Modified;
        _context.Entry(dto.Person).State = EntityState.Modified;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
