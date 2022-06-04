using MediatR;
using SpaceAdventures.Application.Common.Interfaces;

namespace SpaceAdventures.Application.Common.Queries.Clients;

// Query
public class GetClientsQuery : IRequest<ClientsVm>
{
}

// Handler
public class GetClientsQueryHandler : IRequestHandler<GetClientsQuery, ClientsVm>
{
    private readonly IClientService _clientService;

    public GetClientsQueryHandler(IClientService clientService)
    {
        _clientService = clientService;
    }

    public async Task<ClientsVm> Handle(GetClientsQuery request, CancellationToken cancellationToken)
    {
        return await _clientService.GetAllClients(cancellationToken);
    }
}