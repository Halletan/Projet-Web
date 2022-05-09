using SpaceAdventures.Application.Common.Commands.Airports;
using SpaceAdventures.Application.Common.Queries.Airports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Services.Interfaces
{
    public interface IAirportService
    {
        Task<AirportVm> GetAllAirports(CancellationToken cancellation = default);
        Task<AirportDto> GetAirportById(int airportId, CancellationToken cancellation = default);
        Task<AirportDto> CreateAirport(AirportInput airportInput, CancellationToken cancellation = default);
        Task<AirportDto> UpdateAirport(int airportId, AirportInput airportInput, CancellationToken cancellation = default);
        Task DeleteAirport(int airportId, CancellationToken cancellation = default);
        Task<bool> AirportExists(string name,int idplanet);
        bool AirportExists(int? id, AirportInput airportInput);
        bool PlanetExists(int id);
    }
}
