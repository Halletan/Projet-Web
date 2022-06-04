using SpaceAdventures.Application.Common.Commands.Aircrafts;
using SpaceAdventures.Application.Common.Queries.Aircrafts;

namespace SpaceAdventures.Application.Common.Interfaces;

public interface IAircraftService
{
    Task<AircraftsVm> GetAllAircrafts(CancellationToken cancellation = default);
    Task<AircraftDto> GetAircraftById(int aircraftId, CancellationToken cancellation = default);
    Task<AircraftDto> CreateAircraft(AircraftInput aircraftInput, CancellationToken cancellation = default);

    Task<AircraftDto> UpdateAircraft(int aircraftId, AircraftInput aircraftInput,
        CancellationToken cancellation = default);

    Task DeleteAircraft(int aircraftId, CancellationToken cancellation = default);
    //bool AircraftExists(int? id, AircraftInput airportInput);
}