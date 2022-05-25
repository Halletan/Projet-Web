using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Mappings;
using SpaceAdventures.Application.Common.Models;

namespace SpaceAdventures.Application.Common.Queries.Clients.GetClientsWithPagination;

public class GetClientsWithPaginationQuery : IRequest<PaginatedList<ClientsBriefDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class
    GetClientsWithPaginationQueryHandler : IRequestHandler<GetClientsWithPaginationQuery,
        PaginatedList<ClientsBriefDto>>
{
    private readonly ISpaceAdventureDbContext _context;
    private readonly IMapper _mapper;

    public GetClientsWithPaginationQueryHandler(ISpaceAdventureDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ClientsBriefDto>> Handle(GetClientsWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        var paginatedList = await _context.Clients
            .ProjectTo<ClientsBriefDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
        return paginatedList;
    }
}