using AutoMapper;
using ColegioMozart.Application.Common.Exceptions;
using ColegioMozart.Application.Common.Interfaces;
using ColegioMozart.Domain.Entities;
using MediatR;

namespace ColegioMozart.Application.AcademicLevels.Commands.CreateAcademicLevel;

public class CreateAcademicLevelCommand : IRequest
{
    public string Level { get; set; }
    public int AcademicScaleId { get; set; }
    public int? PreviousAcademicLevelId { get; set; }
}

public class CreateAcademicLevelCommandHandler : IRequestHandler<CreateAcademicLevelCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateAcademicLevelCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(CreateAcademicLevelCommand request, CancellationToken cancellationToken)
    {

        if (!_context.AcademicScales.Any(o => o.Id == request.AcademicScaleId))
        {
            throw new NotFoundException(nameof(EAcademicScale), request.AcademicScaleId);
        }

        await _context.AcademicLevels.AddAsync(new EAcademicLevel
        {
            Level = request.Level,
            AcademicScaleId = request.AcademicScaleId,
            PreviousAcademicLevelId = request.PreviousAcademicLevelId
        });

        await _context.SaveChangesAsync(cancellationToken);

        return await Unit.Task;
    }
}