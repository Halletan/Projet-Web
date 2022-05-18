using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using SpaceAdventures.Application.Common.Queries.Planets;

namespace SpaceAdventures.Application.Common.Queries.Airports
{
    public class AirportDto:IMapFrom<Airport>
    {
        public int IdAirport { get; set; }
        public string Name { get; set; }
        public int IdPlanet { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Airport, AirportDto>().ReverseMap(); ;
        }
    }
}
