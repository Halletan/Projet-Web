﻿using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Queries.Aircrafts
{
    public class AircraftDto : IMapFrom<Aircraft>
    {
        public int IdAircraft { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int NumberOfSeats { get; set; }

        public virtual ICollection<Flight> Flights { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Aircraft, AircraftDto>().ReverseMap();
        }
    }
}
