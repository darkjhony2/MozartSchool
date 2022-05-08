using AutoMapper;
using AutoMapper.QueryableExtensions;
using ColegioMozart.Application.Common.Interfaces;
using ColegioMozart.Domain.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ColegioMozart.Application.Common.CommonCRUD.Queries;

public class GetEntitiesQuery : IRequest<GetEntityDTO>
{
    public string View { get; set; }
}

public class GetEntitiesQueryHandler : IRequestHandler<GetEntitiesQuery, GetEntityDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetEntitiesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetEntityDTO> Handle(GetEntitiesQuery request, CancellationToken cancellationToken)
    {
       var view = await _context
            .Views
            .Where(x => x.Name == request.View)
            .Include(x=> x.Entity)
            .FirstOrDefaultAsync();



        Type typeEntity = AssemblyDomainExtensions.GetTypeDomain(view.Entity.EntityFullName);
        Type typeDTO = Type.GetType(view.Entity.EntityDtoFullName);

        IQueryable dbSet = _context.GetQueryable(typeEntity);

        var project = (IQueryable<object>)dbSet
            .ProjectTo(typeDTO, _mapper.ConfigurationProvider);

        return await Task.FromResult(
                new GetEntityDTO()
                {
                    Models = new List<object>(project),
                    View = view
                });
    }


}