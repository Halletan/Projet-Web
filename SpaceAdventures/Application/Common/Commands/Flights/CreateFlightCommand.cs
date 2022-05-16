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
    public record CreateFlightCommand(FlightInput flightInput) : IRequest<FlightDto>;

    public class CreateFlightCommandHandler : IRequestHandler<CreateFlightCommand, FlightDto>
    {
        private readonly IFlightService _flightService;

        public CreateFlightCommandHandler(IFlightService flightService)
        {
            _flightService = flightService;
        }

        public async Task<FlightDto> Handle(CreateFlightCommand request, CancellationToken cancellationToken)
        {
            return await _flightService.CreateFlight(request.flightInput, cancellationToken);
        }
    }
}
