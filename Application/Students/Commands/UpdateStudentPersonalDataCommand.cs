using ColegioMozart.Application.Common.Exceptions;
using ColegioMozart.Application.Students.Dtos;
using ColegioMozart.Domain.Entities;

namespace ColegioMozart.Application.Students.Commands;

[Authorize]
public class UpdateStudentPersonalDataCommand : IRequest
{
    public Guid StudentId { get; set; }
    public UpdateStudentResource Resource { get; set; }
}

public class UpdateStudentPersonalDataCommandHandler : IRequestHandler<UpdateStudentPersonalDataCommand>
{
    private readonly ILogger<UpdateStudentPersonalDataCommandHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public UpdateStudentPersonalDataCommandHandler(
        ILogger<UpdateStudentPersonalDataCommandHandler> logger,
        IApplicationDbContext context,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(UpdateStudentPersonalDataCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Update student personal data @{resource} with userID {}", request.Resource, request.StudentId);

        var entity = await _context.Students.Where(x => x.Id == request.StudentId)
            .FirstOrDefaultAsync();

        if (entity == null)
        {
            throw new NotFoundException("Estudiante", request.StudentId);
        }

        var dto = _mapper.Map<EPerson>(request.Resource);
        dto.Id = entity.PersonId;

        _context.Persons.Attach(dto); // (or context.Entity.Attach(entity);)
        _context.Entry(dto).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
