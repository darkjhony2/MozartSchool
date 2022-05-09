using AutoMapper;
using ColegioMozart.Application.Common.Interfaces;
using ColegioMozart.Domain.Utils;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ColegioMozart.Application.Common.CommonCRUD.Commands;

public class CreateEntityCommand : IRequest
{
    public Dictionary<string, string> Parameters { get; set; }
    public EView View { get; set; }
}


public class CreateEntityCommandHandler : IRequestHandler<CreateEntityCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateEntityCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(CreateEntityCommand request, CancellationToken cancellationToken)
    {
        var typeCreate = Type.GetType(request.View.Entity.CreateEntityFullName);
        var typeEntity = AssemblyDomainExtensions.GetTypeDomain(request.View.Entity.EntityFullName);

        var entity = Activator.CreateInstance(typeCreate);

        foreach (var prop in typeCreate.GetProperties())
        {
            prop.SetValue(entity, Convert.ChangeType(request.Parameters[prop.Name], prop.PropertyType), null);
        }

        var context = new ValidationContext(entity);
        var validationResults = new List<ValidationResult>();
        bool isValid = Validator.TryValidateObject(entity, context, validationResults, true);

        if (!isValid)
        {
            //TODO VALIDATE ERRORS
        }

        var objEntity = _mapper.Map(entity, typeCreate, typeEntity);

        _context.Attach(objEntity);
        _context.Entry(objEntity).State = Microsoft.EntityFrameworkCore.EntityState.Added;

        await _context.SaveChangesAsync(cancellationToken);

        return await Task.FromResult(Unit.Value);
    }
}