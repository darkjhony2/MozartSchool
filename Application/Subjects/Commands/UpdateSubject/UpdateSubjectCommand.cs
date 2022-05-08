using ColegioMozart.Application.Common.Exceptions;
using ColegioMozart.Application.Common.Interfaces;
using ColegioMozart.Application.Subjects.Queries;
using ColegioMozart.Domain.Entities;
using MediatR;

namespace ColegioMozart.Application.Subjects.Commands.UpdateSubject;

public class UpdateSubjectCommand : IRequest
{
    public Guid Id { get; init; }

    public ESubjectDTO Dto { get; init; }
}


public class UpdateSubjectCommandHandler : IRequestHandler<UpdateSubjectCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateSubjectCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Subjects
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(ESubject), request.Id);
        }

        if (_context.Subjects.Any(x => x.Name.Equals(request.Dto.Name) && x.Id != request.Id))
        {
            throw new EntityAlreadyExistException("Name");
        }

        entity.Name = request.Dto.Name;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
