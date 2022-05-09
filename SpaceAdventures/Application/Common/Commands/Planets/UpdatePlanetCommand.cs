using MediatR;
using SpaceAdventures.Application.Common.Queries.Planets;
using SpaceAdventures.Application.Common.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Commands.Planets
{
    public record UpdatePlanetCommand(int Id, PlanetInput planetInput) : IRequest<PlanetDto>;

    public class UpdateClientCommandHandler : IRequestHandler<UpdatePlanetCommand, PlanetDto>
    {

        private readonly IPlanetService _planetService;

        public UpdateClientCommandHandler(IPlanetService planetService)
        {
            _planetService = planetService;
        }

        public async Task<PlanetDto> Handle(UpdatePlanetCommand request, CancellationToken cancellationToken)
        {
            return await _planetService.UpdatePlanet(request.Id, request.planetInput, cancellationToken);
        }
    }
}
