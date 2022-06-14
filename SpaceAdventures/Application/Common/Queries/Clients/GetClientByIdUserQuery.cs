
using MediatR;
using SpaceAdventures.Application.Common.Interfaces;

namespace SpaceAdventures.Application.Common.Queries.Clients
{
    public record GetClientByIdUserQuery(int Id) : IRequest<ClientDto>;

    public class GetClientByIdUserQueryHandler : IRequestHandler<GetClientByIdUserQuery, ClientDto>
    {
        private readonly IClientService _clientService;

        public GetClientByIdUserQueryHandler(IClientService clientService)
        {
            _clientService = clientService;
        }

        public async Task<ClientDto> Handle(GetClientByIdUserQuery request, CancellationToken cancellationToken)
        {
            return await _clientService.GetClientByIdUser(request.Id, cancellationToken);
        }
    }
}
