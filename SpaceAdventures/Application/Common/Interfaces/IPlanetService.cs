using SpaceAdventures.Application.Common.Commands.Planets;
using SpaceAdventures.Application.Common.Queries.Planets;

namespace SpaceAdventures.Application.Common.Interfaces;

public interface IPlanetService
{
    Task<PlanetVm> GetAllPlanets(CancellationToken cancellation = default);
    Task<PlanetDto> GetPlanetById(int planetId, CancellationToken cancellation = default);
    Task<PlanetDto> CreatePlanet(PlanetInput planetInput, CancellationToken cancellation = default);
    Task<PlanetDto> UpdatePlanet(int planetId, PlanetInput planetInput, CancellationToken cancellation = default);
    Task DeletePlanet(int planetId, CancellationToken cancellation = default);
    Task<bool> PlanetExists(string name);
    bool PlanetExists(int id, PlanetInput planetInput);
    Task<PlanetDto> GetPlanetByName(string planetName, CancellationToken cancellation = default);
}