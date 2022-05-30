using ColegioMozart.Application.Common.Exceptions;
using ColegioMozart.Domain.Entities;

namespace ColegioMozart.Application.Teachers.Queries;

public class GetTeacherByIdQuery : IRequest<TeacherDTO>
{
    public Guid Id { get; set; }
}

public class GetTeacherByIdQueryHandler : IRequestHandler<GetTeacherByIdQuery, TeacherDTO>
{
    private readonly ILogger<GetTeacherByIdQueryHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTeacherByIdQueryHandler(
        ILogger<GetTeacherByIdQueryHandler> logger,
        IApplicationDbContext context,
        IMapper mapper)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
    }

    public async Task<TeacherDTO> Handle(GetTeacherByIdQuery request, CancellationToken cancellationToken)
    {
        var teacher = await _context
            .Teachers
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .ProjectTo<TeacherDTO>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        if (teacher == null)
        {
            throw new NotFoundException(nameof(ETeacher), request.Id);
        }

        return teacher;
    }
}