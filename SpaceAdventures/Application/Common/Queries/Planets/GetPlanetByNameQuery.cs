using MediatR;
using SpaceAdventures.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Queries.Planets
{
    public record GetPlanetByNameQuery(string name) : IRequest<PlanetDto>;

    public class GetPlanetByNameCommandHandler : IRequestHandler<GetPlanetByNameQuery, PlanetDto>
    {
        private readonly IPlanetService _planetService;

        public GetPlanetByNameCommandHandler(IPlanetService planetService)
        {
            _planetService = planetService;
        }

        public async Task<PlanetDto> Handle(GetPlanetByNameQuery request, CancellationToken cancellationToken)
        {
            return await _planetService.GetPlanetByName(request.name, cancellationToken);
        }
    }
}