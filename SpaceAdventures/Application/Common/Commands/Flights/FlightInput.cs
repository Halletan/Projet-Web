using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Commands.Flights
{
    public class FlightInput : IMapFrom<Flight>
    {
        public int IdFlight { get; set; }
        public int FlightStatus { get; set; }
        public int IdAircraft { get; set; }
        public double Price { get; set; }
        public int IdItinerary { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }

        public virtual ICollection<AircraftSeat> AircraftSeats { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FlightInput, Flight>().ReverseMap();
        }
    }
}
