using MediatR;
using SpaceAdventures.Application.Common.Queries.Aircrafts;
using SpaceAdventures.Application.Common.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Commands.Aircrafts
{
    public record CreateAircraftCommand(AircraftInput AircraftInput) : IRequest<AircraftDto>;

    public class CreateAircraftCommandHandler : IRequestHandler<CreateAircraftCommand, AircraftDto>
    {
        private readonly IAircraftService _aircraftService;     

        public CreateAircraftCommandHandler(IAircraftService aircraftService)
        {
            _aircraftService = aircraftService;
        }

        public async Task<AircraftDto> Handle(CreateAircraftCommand request, CancellationToken cancellationToken)
        {
            return await _aircraftService.CreateAircraft(request.AircraftInput, cancellationToken);
        }
    }
}
