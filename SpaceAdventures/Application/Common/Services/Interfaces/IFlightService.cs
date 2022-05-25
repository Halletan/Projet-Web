using SpaceAdventures.Application.Common.Commands.Flights;
using SpaceAdventures.Application.Common.Queries.Flights;

namespace SpaceAdventures.Application.Common.Services.Interfaces;

public interface IFlightService
{
    Task<FlightsVm> GetAllFlights(CancellationToken cancellation = default);
    Task<FlightDto> GetFlightById(int flightId, CancellationToken cancellation = default);
    Task<FlightDto> CreateFlight(FlightInput flightInput, CancellationToken cancellation = default);
    Task<FlightDto> UpdateFlight(int flightId, FlightInput flightInput, CancellationToken cancellation = default);

    Task DeleteFlight(int flightId, CancellationToken cancellation = default);

    // Task<bool> FlightExists(int flightid);
    bool AircraftExists(int id);
    bool ItineraryExists(int id);
}