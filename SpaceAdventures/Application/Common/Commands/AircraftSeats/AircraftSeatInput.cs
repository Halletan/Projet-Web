using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Commands.AircraftSeats
{
    public class AircraftSeatInput : IMapFrom<AircraftSeat>
    {
        public int IdAircraftSeat { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int IdBooking { get; set; }
        public string PassengerLastName { get; set; }
        public string PassengerFirstName { get; set; }
        public int IdFlight { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AircraftSeatInput, AircraftSeat>();

        }

    }
}
