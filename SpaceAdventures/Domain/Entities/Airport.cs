﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Airport
    {
        public Airport()
        {
            ItineraryIdAiport2Navigations = new HashSet<Itinerary>();
            ItineraryIdAirport1Navigations = new HashSet<Itinerary>();
        }

        public int IdAirport { get; set; }
        public string Name { get; set; }
        public int IdPlanet { get; set; }

        public virtual Planet IdPlanetNavigation { get; set; }
        public virtual ICollection<Itinerary> ItineraryIdAiport2Navigations { get; set; }
        public virtual ICollection<Itinerary> ItineraryIdAirport1Navigations { get; set; }
    }
}