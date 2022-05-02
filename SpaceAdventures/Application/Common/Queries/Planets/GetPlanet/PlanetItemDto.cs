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
    public class PlanetItemDto :IMapFrom<Planet>
    {
        public int IdPlanet { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Airport> Airports { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Planet, PlanetItemDto>();
        }
    }
}
