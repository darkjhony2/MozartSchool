using AutoMapper;
using AutoMapper.QueryableExtensions;
using ColegioMozart.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ColegioMozart.Application.Common.CommonCRUD.Queries;

public class GetEntitiesMenuQuery : IRequest<List<ViewMenuDTO>>
{
}

public class GetEntitiesMenuQueryHandler : IRequestHandler<GetEntitiesMenuQuery, List<ViewMenuDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetEntitiesMenuQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ViewMenuDTO>> Handle(GetEntitiesMenuQuery request, CancellationToken cancellationToken)
    {
        return await _context
             .Views
              .ProjectTo<ViewMenuDTO>(_mapper.ConfigurationProvider)
              .ToListAsync();
    }
}
