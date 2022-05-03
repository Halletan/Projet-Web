using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SpaceAdventures.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Queries.Planets.GetPlanet
{
    public record GetPlanetByIdQuery(int Id) : IRequest<PlanetVm>;

    public class GetPlanetByIdCommandHandler : IRequestHandler<GetPlanetByIdQuery, PlanetVm>
    {
        private readonly ISpaceAdventureDbContext _context;
        private readonly IMapper _mapper;

        public GetPlanetByIdCommandHandler(IMapper mapper, ISpaceAdventureDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PlanetVm> Handle(GetPlanetByIdQuery request, CancellationToken cancellationToken)
        {
            var planet = await _context.Planets.FirstOrDefaultAsync(p => p.IdPlanet == request.Id , cancellationToken);

            if (planet == null)
            {
                throw new Exception("Planet does not exist"); 
            }

            return _mapper
                .Map<Planet, PlanetVm>(planet);

        }
    }
}
