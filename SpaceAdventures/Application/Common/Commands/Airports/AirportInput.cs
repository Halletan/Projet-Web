using Application.Common.Interfaces;
using AutoMapper;
using SpaceAdventures.Domain.Entities;

namespace SpaceAdventures.Application.Common.Commands.Airports;

public class AirportInput : IMapFrom<Airport>
{
    public int IdAirport { get; set; }
    public string Name { get; set; }
    public int IdPlanet { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<AirportInput, Airport>();
    }
}