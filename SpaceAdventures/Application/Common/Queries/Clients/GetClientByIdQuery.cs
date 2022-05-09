using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Services.Interfaces;
using MediatR;

namespace SpaceAdventures.Application.Common.Queries.Clients
{
    public record GetclientByIdQuery(int Id) : IRequest<ClientDto>;

    public class GetClientByIdQueryHandler : IRequestHandler<GetclientByIdQuery, ClientDto>
    {
        private readonly IClientService _clientService;

        public GetClientByIdQueryHandler(IClientService clientService)
        {
            _clientService = clientService;
        }

        public async Task<ClientDto> Handle(GetclientByIdQuery request, CancellationToken cancellationToken)
        {
            return await _clientService.GetClientById(request.Id, cancellationToken);
        }
    }
}
