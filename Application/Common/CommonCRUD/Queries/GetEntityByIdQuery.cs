using AutoMapper;
using ColegioMozart.Application.Common.Interfaces;
using ColegioMozart.Domain.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ColegioMozart.Application.Common.CommonCRUD.Queries;

public class GetEntityByIdQuery : IRequest<Tuple<object, EView>>
{
    public string Id { get; set; }
    public string View { get; set; }
}

public class GetEntityByIdRequestHandler : IRequestHandler<GetEntityByIdQuery, Tuple<object, EView>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetEntityByIdRequestHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Tuple<object, EView>> Handle(GetEntityByIdQuery request, CancellationToken cancellationToken)
    {
        var view = await _context
          .Views
          .AsNoTracking()
          .Where(x => x.Name == request.View)
          .Include(x => x.Entity)
          .FirstOrDefaultAsync();

        Type typeEntity = AssemblyDomainExtensions.GetTypeDomain(view.Entity.EntityFullName);
        Type typeDTO = Type.GetType(view.Entity.EntityDtoFullName);

        IQueryable dbSet = _context.GetQueryable(typeEntity);

        var propId = typeEntity.GetProperties().Where(x => x.Name == "Id").FirstOrDefault();

        var query = propId.GetQuery(dbSet, request.Id);

        var childProps = GetIncludeProps(typeEntity);
        var queryFinder = query.OfType<object>();

        foreach (var childProp in childProps)
        {
            queryFinder = queryFinder.Include(childProp);
        }

        var response = await queryFinder.FirstOrDefaultAsync();


        return Tuple.Create(_mapper.Map(response, typeEntity, typeDTO), view);
    }

    private List<string> GetIncludeProps(Type type)
    {
        var includeProps = new List<string>();

        var childProps = type.GetProperties().Where(x => !x.PropertyType.Namespace.StartsWith("System") && x.PropertyType != type)
            .ToList();

        includeProps.AddRange(childProps.Select(x => x.Name));

        foreach (var childProp in childProps.Where(x => !x.PropertyType.Namespace.StartsWith("System")))
        {
            includeProps.AddRange(GetIncludeProps(childProp.PropertyType).Select(x => $"{childProp.Name}.{x}").ToList());
        }

        return includeProps;
    }

}
