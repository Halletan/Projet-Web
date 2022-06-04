using Application.Common.Interfaces;
using AutoMapper;
using SpaceAdventures.Domain.Entities;

namespace SpaceAdventures.Application.Common.Queries.Bookings;

public class BookingDto : IMapFrom<Booking>
{
    public int IdBooking { get; set; }
    public int IdFlight { get; set; }
    public int IdClient { get; set; }
    public int NbSeats { get; set; }

    

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Booking, BookingDto>().ReverseMap();
    }
}