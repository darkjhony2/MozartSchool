using ColegioMozart.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioMozart.Application.Shifts.Commands;

public class CreateShiftCommand : IRequest
{
    public string Name { get; set; }
}

public class CreateShiftCommandHandler : IRequestHandler<CreateShiftCommand>
{
    private readonly ILogger<CreateShiftCommandHandler> _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateShiftCommandHandler(
        ILogger<CreateShiftCommandHandler> logger,
        ICurrentUserService currentUserService,
        IApplicationDbContext context,
        IMapper mapper)
    {
        _logger = logger;
        _currentUserService = currentUserService;
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(CreateShiftCommand request, CancellationToken cancellationToken)
    {
        var shift = new EShift
        {
            Name = request.Name
        };

        _context.Shifts.Add(shift);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}