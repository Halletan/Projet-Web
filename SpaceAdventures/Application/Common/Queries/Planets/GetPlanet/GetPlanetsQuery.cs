using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Queries.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Queries.Planets.GetPlanet
{
    public record GetPlanetsQuery : IRequest<PlanetVm>;

    public class GetPlanetsQueryHandler : IRequestHandler<GetPlanetsQuery, PlanetVm>
    {
        private readonly ISpaceAdventureDbContext _context;
        private readonly IMapper _mapper;

        public GetPlanetsQueryHandler(IMapper mapper, ISpaceAdventureDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<PlanetVm> Handle(GetPlanetsQuery request, CancellationToken cancellationToken)
        {
            return new PlanetVm
            {
                ListOfPlanets = await _context.Planets
                    .ProjectTo<PlanetDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
