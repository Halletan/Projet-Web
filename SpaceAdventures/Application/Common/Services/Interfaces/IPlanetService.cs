using SpaceAdventures.Application.Common.Commands.Planets;
using SpaceAdventures.Application.Common.Queries.Planets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Services.Interfaces
{
    public interface IPlanetService
    {
        Task<PlanetVm> GetAllPlanets(CancellationToken cancellation = default);
        Task<PlanetDto> GetPlanetById(int planetId, CancellationToken cancellation = default);
        Task<PlanetDto> CreatePlanet(PlanetInput planetInput, CancellationToken cancellation = default);
        Task<PlanetDto> UpdatePlanet(int planetId, PlanetInput planetInput, CancellationToken cancellation = default);
        Task DeletePlanet(int planetId, CancellationToken cancellation = default);
        Task<bool> PlanetExists(string name);
        bool PlanetExists(int id,PlanetInput planetInput);
    }
}
