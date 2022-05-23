using Application.Common.Interfaces;
using Domain.Entities;

namespace SpaceAdventures.Application.Common.Queries.Planets;

public class PlanetVm : IMapFrom<Planet>
{
    public IList<PlanetDto> PlanetsList { get; set; }
}