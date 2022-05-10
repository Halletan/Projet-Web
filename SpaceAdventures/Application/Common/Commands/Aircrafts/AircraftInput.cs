using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Commands.Aircrafts
{
    public class AircraftInput : IMapFrom<Aircraft>
    {

        public int IdAircraft { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int NumberOfSeats { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AircraftInput, Aircraft>();
        }
    }
}
