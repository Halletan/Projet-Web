using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using SpaceAdventures.Application.Common.Queries.Itineraries;

namespace SpaceAdventures.Application.Common.Commands.Itineraries
{
    public class ItineraryInput : IMapFrom<Itinerary>
        {
            public int IdItinerary { get; set; }
            public int IdAirport1 { get; set; }
            public int IdAirport2 { get; set; }
            public double Rate { get; set; }
            public virtual ICollection<Flight> Flights { get; set; }

            public void Mapping(Profile profile)
            {
                profile.CreateMap<ItineraryInput, Itinerary>();
            }
        }
}
