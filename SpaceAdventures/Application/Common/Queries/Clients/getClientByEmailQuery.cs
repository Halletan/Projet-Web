using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Queries.Clients;

namespace SpaceAdventures.Application.Common.Queries.Clients;
public record GetClientByEmailQuery(string Email) : IRequest<ClientDto>;

public class GetClientByEmailQueryHandler : IRequestHandler<GetClientByEmailQuery, ClientDto>
{
    private readonly IClientService _clientService;

    public GetClientByEmailQueryHandler(IClientService clientService)
    {
        _clientService = clientService;
    }

    public async Task<ClientDto> Handle(GetClientByEmailQuery request, CancellationToken cancellationToken)
    {
        return await _clientService.GetClientByEmail(request.Email, cancellationToken);
    }
}