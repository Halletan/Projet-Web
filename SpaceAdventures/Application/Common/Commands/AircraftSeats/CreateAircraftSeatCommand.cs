using MediatR;
using SpaceAdventures.Application.Common.Queries.AircraftSeats;
using SpaceAdventures.Application.Common.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Commands.AircraftSeats
{
    public record CreateAircraftSeatCommand(AircraftSeatInput aircraftSeatInput) : IRequest<AircraftSeatDto>;

    public class CreateAircraftSeatCommandHandler : IRequestHandler<CreateAircraftSeatCommand, AircraftSeatDto>
    {
        private readonly IAircraftSeatService _aircraftSeatService;

        public CreateAircraftSeatCommandHandler(IAircraftSeatService aircraftSeatService)
        {
            _aircraftSeatService = aircraftSeatService;
        }

        public async Task<AircraftSeatDto> Handle(CreateAircraftSeatCommand request, CancellationToken cancellationToken)
        {
            return await _aircraftSeatService.CreateAircraftSeat(request.aircraftSeatInput, cancellationToken);
        }
    }
}
