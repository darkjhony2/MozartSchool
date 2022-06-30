using ColegioMozart.Application.AcademicLevels.Queries.GetAcademicLevelById;
using ColegioMozart.Application.AcademicScale.Queries;
using ColegioMozart.Application.Common.Exceptions;
using ColegioMozart.Application.Common.Security;
using ColegioMozart.Domain.Entities;

namespace ColegioMozart.Application.AcademicLevels.Commands.CreateAcademicLevel;

[Authorize]
public class UpdateAcademicLevelCommand : IRequest
{
    public int AcademicLevelId { get; set; }

    public UpdateAcademicLevelResource Resource { get; set; }

    public record UpdateAcademicLevelResource(string Level, int scaleId, int? previousAcademicLevelId);
}

public class UpdateAcademicLevelCommandHandler : IRequestHandler<UpdateAcademicLevelCommand>
{
    private readonly ILogger<UpdateAcademicLevelCommandHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ISender _sender;

    public UpdateAcademicLevelCommandHandler(
        ILogger<UpdateAcademicLevelCommandHandler> logger,
        IApplicationDbContext context,
        IMapper mapper,
        ISender sender)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
        _sender = sender;
    }

    public async Task<Unit> Handle(UpdateAcademicLevelCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Se actualizará un nivel academico : @{request}", request);

        var found = await _sender.Send(new GetAcademicLevelByIdQuery
        {
            Id = request.AcademicLevelId
        });


        if (await _context.AcademicLevels.Where(x => x.Id != found.Id && x.Level == request.Resource.Level).AnyAsync())
        {
            throw new EntityAlreadyExistException("Ya existe un grado con el mismo nombre");
        }

        if(request.Resource.previousAcademicLevelId != null)
        {
            await _sender.Send(new GetAcademicLevelByIdQuery
            {
                Id = request.Resource.previousAcademicLevelId.Value
            });
        }

        await _sender.Send(new GetAcademicScaleByIdQuery { Id = request.Resource.scaleId });

        var entity = new EAcademicLevel
        {
            Id = request.AcademicLevelId,
            AcademicScaleId = request.Resource.scaleId,
            PreviousAcademicLevelId = request.Resource.previousAcademicLevelId,
            Level = request.Resource.Level
        };


        _context.AcademicLevels.Update(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
