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
        private readonly IAircraftService _AircraftService;

        public CreateAircraftCommandHandler(IAircraftService AircraftService)
        {
            _AircraftService = AircraftService;
        }

        public async Task<AircraftDto> Handle(CreateAircraftCommand request, CancellationToken cancellationToken)
        {
            return await _AircraftService.CreateAircraft(request.AircraftInput, cancellationToken);
        }
    }
}
