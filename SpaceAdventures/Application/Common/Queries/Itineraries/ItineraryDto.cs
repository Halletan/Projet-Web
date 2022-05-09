using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace SpaceAdventures.Application.Common.Queries.Itineraries
{
    public class ItineraryDto : IMapFrom<Itinerary>
    {
        public int IdItinerary { get; set; }
        public int IdAirport1 { get; set; }
        public int IdAirport2 { get; set; }
        public double Rate { get; set; }
        public virtual ICollection<Flight> Flights { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Itinerary, ItineraryDto>().ReverseMap();
        }
    }
}
