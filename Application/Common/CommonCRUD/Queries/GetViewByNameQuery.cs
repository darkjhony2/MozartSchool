using AutoMapper;
using ColegioMozart.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ColegioMozart.Application.Common.CommonCRUD.Queries;

public class GetViewByNameQuery : IRequest<EView>
{
    public string View { get; set; }
}

public class GetViewByNameQueryHandler : IRequestHandler<GetViewByNameQuery, EView>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetViewByNameQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<EView> Handle(GetViewByNameQuery request, CancellationToken cancellationToken)
    {
        return await _context
          .Views
          .AsNoTracking()
          .Where(x => x.Name == request.View)
          .Include(x => x.Entity)
          .FirstOrDefaultAsync(cancellationToken);
    }
}
