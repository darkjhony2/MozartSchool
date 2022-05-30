using AutoMapper;
using AutoMapper.QueryableExtensions;
using ColegioMozart.Application.Common.Exceptions;
using ColegioMozart.Application.Common.Interfaces;
using ColegioMozart.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ColegioMozart.Application.AcademicLevels.Queries.GetAcademicLevelById;

public class GetAcademicLevelByIdQuery : IRequest<AcademicLevelDTO>
{
    public int Id { get; set; }
}

public class GetAcademicLevelByIdQueryHandler : IRequestHandler<GetAcademicLevelByIdQuery, AcademicLevelDTO>
{
    private readonly ILogger<GetAcademicLevelByIdQueryHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAcademicLevelByIdQueryHandler(
        ILogger<GetAcademicLevelByIdQueryHandler> logger,
        IApplicationDbContext context,
        IMapper mapper)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
    }

    public async Task<AcademicLevelDTO> Handle(GetAcademicLevelByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.AcademicLevels
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .ProjectTo<AcademicLevelDTO>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if(result == null)
        {
            throw new NotFoundException(nameof(EAcademicLevel), request.Id);
        }

        return result;
    }
}