using ColegioMozart.Application.Common.Exceptions;
using ColegioMozart.Application.Common.Interfaces;
using ColegioMozart.Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ColegioMozart.Application.Subjects.Commands.CreateSubject;

[Authorize]
public class CreateSubjectCommand : IRequest<Guid>
{
    [Required]
    [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]+[0-9]*$", ErrorMessage = "Solo se permiten letras y números")]
    [Display(Name = "Nombre")]
    public string Name { get; set; }
}

public class CreateSubjectCommandHandler : IRequestHandler<CreateSubjectCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateSubjectCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
    {
        var entity = new ESubject();
        entity.Name = request.Name;

        if (_context.Subjects.Any(x => x.Name.Equals(request.Name))){
            throw new EntityAlreadyExistException("Name");
        }

        _context.Subjects.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}