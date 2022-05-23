using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace SpaceAdventures.Application.Common.Queries.Flights;

public class FlightDto : IMapFrom<Flight>
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
        profile.CreateMap<Flight, FlightDto>().ReverseMap();
    }
}