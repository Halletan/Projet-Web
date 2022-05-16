using MediatR;
using SpaceAdventures.Application.Common.Queries.Flights;
using SpaceAdventures.Application.Common.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Commands.Flights
{
    public record UpdateFlightCommand(int Id, FlightInput flightInput) : IRequest<FlightDto>;


    public class UpdateFlightCommandHandler : IRequestHandler<UpdateFlightCommand, FlightDto>
    {

        private readonly IFlightService _flightService;

        public UpdateFlightCommandHandler(IFlightService flightService)
        {
            _flightService = flightService;
        }

        public async Task<FlightDto> Handle(UpdateFlightCommand request, CancellationToken cancellationToken)
        {
            return await _flightService.UpdateFlight(request.Id, request.flightInput, cancellationToken);
        }
    }
}
