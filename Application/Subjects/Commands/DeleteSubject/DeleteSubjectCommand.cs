using ColegioMozart.Application.Common.Exceptions;
using ColegioMozart.Application.Common.Interfaces;
using ColegioMozart.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ColegioMozart.Application.Subjects.Commands.DeleteSubject;

[Authorize]
public class DeleteSubjectCommand : IRequest
{
    public Guid Id { get; set; }
}


public class DeleteSubjectCommandHandler : IRequestHandler<DeleteSubjectCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteSubjectCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Subjects
               .Where(l => l.Id == request.Id)
               .SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(ESubject), request.Id);
        }

        _context.Subjects.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}