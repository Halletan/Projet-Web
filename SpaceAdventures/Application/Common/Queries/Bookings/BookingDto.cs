using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Queries.Bookings
{
    public  class BookingDto
    {

        public int IdBooking { get; set; }
        public int IdFlight { get; set; }
        public int IdClient { get; set; }
        public double BookingAmount { get; set; }

        public virtual ICollection<AircraftSeat> AircraftSeats { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Booking, BookingDto>().ReverseMap();
        }

    }
}
