using Application.Common.Services.Interfaces;
using MediatR;

namespace SpaceAdventures.Application.Common.Queries.Clients;

public record GetClientByIdQuery(int Id) : IRequest<ClientDto>;

public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, ClientDto>
{
    private readonly IClientService _clientService;

    public GetClientByIdQueryHandler(IClientService clientService)
    {
        _clientService = clientService;
    }

    public async Task<ClientDto> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
    {
        return await _clientService.GetClientById(request.Id, cancellationToken);
    }
}