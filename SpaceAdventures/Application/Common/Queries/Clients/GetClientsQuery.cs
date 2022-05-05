using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Services;
using Application.Common.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SpaceAdventures.Application.Common.Interfaces;

namespace SpaceAdventures.Application.Common.Queries.Clients
{
    // Query
    public class GetClientsQuery : IRequest<ClientsVm> { }

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
}
