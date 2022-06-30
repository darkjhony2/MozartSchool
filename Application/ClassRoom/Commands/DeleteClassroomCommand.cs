using ColegioMozart.Application.Common.Exceptions;
using ColegioMozart.Application.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioMozart.Application.ClassRoom.Commands;


[Authorize]
public class DeleteClassroomCommand : IRequest
{
    public Guid ClassroomId { get; set; }
}

public class DeleteClassroomCommandHandler : IRequestHandler<DeleteClassroomCommand>
{
    private readonly ILogger<DeleteClassroomCommandHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public DeleteClassroomCommandHandler(
        ILogger<DeleteClassroomCommandHandler> logger,
        IApplicationDbContext context,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(DeleteClassroomCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Eliminar un salón de clases {}", request.ClassroomId);

        var entity = await _context.ClassRooms
            .Where(x => x.Id == request.ClassroomId)
            .FirstOrDefaultAsync();

        if (entity == null)
        {
            throw new NotFoundException("Salón de clases", request.ClassroomId);
        }

        if (entity.Year != DateTime.Now.Year)
        {
            throw new BusinessRuleException($"No se puede eliminar un salón de clases de un diferente al actual ({DateTime.Now.Year})");
        }

        _context.ClassRooms.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
