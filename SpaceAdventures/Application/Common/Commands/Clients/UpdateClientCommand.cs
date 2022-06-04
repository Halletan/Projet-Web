using MediatR;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Queries.Clients;

namespace SpaceAdventures.Application.Common.Commands.Clients;

public record UpdateClientCommand(int Id, ClientInput clientInput) : IRequest<ClientDto>;

public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, ClientDto>
{
    private readonly IClientService _clientService;

    public UpdateClientCommandHandler(IClientService clientService)
    {
        _clientService = clientService;
    }

    public async Task<ClientDto> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        return await _clientService.UpdateClient(request.Id, request.clientInput, cancellationToken);
    }
}