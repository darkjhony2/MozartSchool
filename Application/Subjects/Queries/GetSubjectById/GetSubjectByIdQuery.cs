using AutoMapper;
using AutoMapper.QueryableExtensions;
using ColegioMozart.Application.Common.Exceptions;
using ColegioMozart.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ColegioMozart.Application.Subjects.Queries.GetSubjectById;

public class GetSubjectByIdQuery : IRequest<ESubjectDTO>
{
    public Guid Id { get; init; }
}

public class GetSubjectByIdQueryHandler : IRequestHandler<GetSubjectByIdQuery, ESubjectDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetSubjectByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ESubjectDTO> Handle(GetSubjectByIdQuery request, CancellationToken cancellationToken)
    {
        var response = await _context.Subjects
              .AsNoTracking()
              .ProjectTo<ESubjectDTO>(_mapper.ConfigurationProvider)
              .Where(x => x.Id == request.Id)
              .FirstOrDefaultAsync();

        if (response == null)
        {
            throw new NotFoundException();
        }

        return response;
    }
}