using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Services.Interfaces;
using MediatR;
using SpaceAdventures.Application.Common.Queries.Clients;

namespace SpaceAdventures.Application.Common.Commands.Clients
{
    public record CreateClientCommand(ClientInput ClientInput) : IRequest<ClientDto>;

    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, ClientDto>
    {
        private readonly IClientService _clientService;

        public CreateClientCommandHandler(IClientService clientService)
        {
            _clientService = clientService;
        }

        public async Task<ClientDto> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
             return await _clientService.CreateClient(request.ClientInput, cancellationToken);
        }
    }
}
    