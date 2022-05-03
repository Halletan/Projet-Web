using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Queries.Planets.GetPlanet
{
    public class PlanetDto : IMapFrom<Planet>
    {
        public int IdPlanet { get; set; }
        public string Name { get; set; }

        // TODO should be a list of AirportDto
        public virtual ICollection<Airport> Airports { get; set; } = new List<Airport>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Planet, PlanetDto>();
        }
    }
}
