using Application.Common.Interfaces;
using SpaceAdventures.Domain.Entities;

namespace SpaceAdventures.Application.Common.Queries.Planets;

public class PlanetVm : IMapFrom<Planet>
{
    public IList<PlanetDto> PlanetsList { get; set; }
}