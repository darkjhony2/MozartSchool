using AutoMapper;
using ColegioMozart.Application.Common.Exceptions;
using ColegioMozart.Application.Common.Interfaces;
using ColegioMozart.Domain.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ColegioMozart.Application.Common.CommonCRUD.Commands;

public class DeleteEntityCommand : IRequest
{
    public string Id { get; set; }
    public string View { get; set; }
}

public class DeleteEntityCommandHandler : IRequestHandler<DeleteEntityCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public DeleteEntityCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(DeleteEntityCommand request, CancellationToken cancellationToken)
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
        var entity = await propId.GetQuery(dbSet, request.Id).OfType<object>().FirstOrDefaultAsync();


        if (entity == null)
        {
            throw new NotFoundException(nameof(typeEntity), request.Id);
        }

        if (_context.Entry(entity).State == EntityState.Detached)
        {
            _context.Attach(entity);
        }

        _context.Entry(entity).State = EntityState.Deleted;

        await _context.SaveChangesAsync(cancellationToken);

        return await Task.FromResult(Unit.Value);
    }

}
