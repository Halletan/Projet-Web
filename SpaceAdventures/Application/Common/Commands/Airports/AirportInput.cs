using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Commands.Airports
{
    public class AirportInput : IMapFrom<Airport>
    {

        public int IdAirport { get; set; }
        public string Name { get; set; }
        public int IdPlanet { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AirportInput, Airport>();

        }
    }
}

