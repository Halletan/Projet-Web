using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace SpaceAdventures.Application.Common.Queries.Bookings;

public class BookingDto : IMapFrom<Booking>
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