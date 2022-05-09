using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Queries.Clients;
using SpaceAdventures.Application.Common.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Queries.Planets
{
    public record GetPlanetsQuery : IRequest<PlanetVm>;

    public class GetPlanetsQueryHandler : IRequestHandler<GetPlanetsQuery, PlanetVm>
    {
        private readonly IPlanetService _planetService;

        public GetPlanetsQueryHandler(IPlanetService planetService)
        {
            _planetService = planetService;
        }

        public async Task<PlanetVm> Handle(GetPlanetsQuery request, CancellationToken cancellationToken)
        {
            return await _planetService.GetAllPlanets(cancellationToken);
        }
    }
}
