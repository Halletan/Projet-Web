using Application.Common.Interfaces;
using AutoMapper;
using SpaceAdventures.Domain.Entities;

namespace SpaceAdventures.Application.Common.Commands.Bookings;

public class BookingInput : IMapFrom<Booking>
{
    public int IdBooking { get; set; }
    public int IdFlight { get; set; }
    public int IdClient { get; set; }
    public int NbSeats { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<BookingInput, Booking>();
    }
}