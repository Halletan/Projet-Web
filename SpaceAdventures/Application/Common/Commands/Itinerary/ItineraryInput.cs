using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Newtonsoft.Json;

using SpaceAdventures.Application.Common.Queries.Itineraries;

namespace SpaceAdventures.Application.Common.Commands.Itinerary
{
    public class ItineraryInput: IMapFrom<Itinerary>
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
