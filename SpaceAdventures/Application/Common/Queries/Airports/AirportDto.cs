using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Queries.Airports
{
    public class AirportDto
    {
        public int IdAirport { get; set; }
        public string Name { get; set; }
        public int IdPlanet { get; set; }
        public virtual ICollection<Itinerary> ItineraryIdAiport2Navigations { get; set; } = new List<Itinerary>();
        public virtual ICollection<Itinerary> ItineraryIdAirport1Navigations { get; set; } = new List<Itinerary>();
    }
}
