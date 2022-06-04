using Application.Common.Interfaces;
using AutoMapper;
using SpaceAdventures.Domain.Entities;

namespace SpaceAdventures.Application.Common.Commands.Itineraries;

public class ItineraryInput : IMapFrom<Itinerary>
{
    public int IdItinerary { get; set; }
    public int IdAirport1 { get; set; }
    public int IdAirport2 { get; set; }
    
    public virtual ICollection<Flight> Flights { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ItineraryInput, Itinerary>();
    }
}