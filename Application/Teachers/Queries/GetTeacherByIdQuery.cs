using ColegioMozart.Application.Common.Exceptions;
using ColegioMozart.Application.Teachers.Dtos;
using ColegioMozart.Domain.Entities;

namespace ColegioMozart.Application.Teachers.Queries;

[Authorize]
public class GetTeacherByIdQuery : IRequest<TeacherDetailDTO>
{
    public Guid Id { get; set; }
}

public class GetTeacherByIdQueryHandler : IRequestHandler<GetTeacherByIdQuery, TeacherDetailDTO>
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

    public async Task<TeacherDetailDTO> Handle(GetTeacherByIdQuery request, CancellationToken cancellationToken)
    {
        var teacher = await _context
            .Teachers
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .ProjectTo<TeacherDetailDTO>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        if (teacher == null)
        {
            throw new NotFoundException(nameof(ETeacher), request.Id);
        }

        return teacher;
    }
}