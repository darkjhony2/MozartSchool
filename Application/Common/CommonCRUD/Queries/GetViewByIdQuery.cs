using AutoMapper;
using ColegioMozart.Application.Common.Interfaces;
using MediatR;

namespace ColegioMozart.Application.Common.CommonCRUD.Queries;

public class GetViewByIdQuery : IRequest<EView>
{
    public string View { get; set; }
}

public class GetViewByIdQueryHandler : IRequestHandler<GetViewByIdQuery, EView>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetViewByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<EView> Handle(GetViewByIdQuery request, CancellationToken cancellationToken)
    {
        return null;

    }
}
