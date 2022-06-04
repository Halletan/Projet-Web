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
    private readonly IClientService _clientService;

    public GetClientsWithPaginationQueryHandler(IClientService clientService)
    {
        _clientService = clientService;
    }

    public async Task<PaginatedList<ClientsBriefDto>> Handle(GetClientsWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        return await _clientService.GetAllClientsWithPagination(request.PageNumber, request.PageSize,
            cancellationToken);
    }
}