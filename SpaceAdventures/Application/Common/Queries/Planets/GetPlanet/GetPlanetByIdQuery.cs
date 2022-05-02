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
    public class GetPlanetByIdQuery : IRequest<PlanetVm>
    {
        public int Id { get; set; }
        public GetPlanetByIdQuery(int id)
        {
            this.Id = id;
        }
    }
    

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
            return new PlanetVm {
                  List = await _context.Planets
                .Where(pId=>pId.IdPlanet==request.Id)
                .ProjectTo<PlanetItemDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
             };
            
        }
    }
}
