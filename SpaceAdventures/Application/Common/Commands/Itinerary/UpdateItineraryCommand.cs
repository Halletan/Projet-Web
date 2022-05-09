using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Services.Interfaces;
using Domain.Entities;
using MediatR;


namespace SpaceAdventures.Application.Common.Commands.Itinerary
{
    public record UpdateAirportCommand(int Id, ItineraryInput ClientInput) : IRequest<ClientDto>;


    public class UpdateClientCommandHandler : IRequestHandler<UpdateAirportCommand, ClientDto>
    {

        private readonly IClientService _clientService;

        public UpdateClientCommandHandler(IClientService clientService)
        {
            _clientService = clientService;
        }

        public async Task<ClientDto> Handle(UpdateAirportCommand request, CancellationToken cancellationToken)
        {
            return await _clientService.UpdateClient(request.Id, request.ClientInput, cancellationToken);
        }
    }
}
        
