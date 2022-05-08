using AutoMapper;
using AutoMapper.QueryableExtensions;
using ColegioMozart.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ColegioMozart.Application.Subjects.Queries;

public class GetSubjectsQuery : IRequest<IList<ESubjectDTO>>
{

}

public class GetSubjectsQueryHandler : IRequestHandler<GetSubjectsQuery, IList<ESubjectDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetSubjectsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IList<ESubjectDTO>> Handle(GetSubjectsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Subjects
               .AsNoTracking()
               .ProjectTo<ESubjectDTO>(_mapper.ConfigurationProvider)
               .OrderBy(t => t.Name)
               .ToListAsync(cancellationToken);
    }
}