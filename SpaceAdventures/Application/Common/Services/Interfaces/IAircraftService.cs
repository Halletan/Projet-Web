
using SpaceAdventures.Application.Common.Commands.Aircrafts;
using SpaceAdventures.Application.Common.Queries.Aircrafts;


namespace SpaceAdventures.Application.Common.Services.Interfaces
{
    public interface IAircraftService
    {
        Task<AircraftsVm> GetAllAircrafts(CancellationToken cancellation = default);
        Task<AircraftDto> GetAircraftById(int AircraftId, CancellationToken cancellation = default);
        Task<AircraftDto> CreateAircraft(AircraftInput AircraftInput, CancellationToken cancellation = default);
        Task<AircraftDto> UpdateAircraft(int AircraftId, AircraftDto AircraftDto, CancellationToken cancellation = default);
        Task DeleteAircraft(int AircraftId, CancellationToken cancellation = default);
        
    }
}
