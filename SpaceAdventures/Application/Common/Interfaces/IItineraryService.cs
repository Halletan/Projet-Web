using SpaceAdventures.Application.Common.Commands.Itineraries;
using SpaceAdventures.Application.Common.Queries.Airports;
using SpaceAdventures.Application.Common.Queries.Itineraries;
using SpaceAdventures.Domain.Entities;

namespace SpaceAdventures.Application.Common.Interfaces;

public interface IItineraryService
{
    Task<ItineraryVm> GetAllItineraries(CancellationToken cancellation = default);
    Task<ItineraryDto> GetItineraryById(int itineraryId, CancellationToken cancellation = default);
    Task<ItineraryDto> CreateItinerary(ItineraryInput itineraryInput, CancellationToken cancellation = default);

    Task<ItineraryDto> UpdateItinerary(int itineraryId, ItineraryInput itineraryInput,
        CancellationToken cancellation = default);

    Task DeleteItinerary(int itineraryId, CancellationToken cancellation = default);
    bool ItineraryExists(ItineraryInput itineraryInput);


    //Task<ItineraryVm> GetItinerariesByDestinationPlanet(string planetName, CancellationToken cancellationToken = default);
    Task<ItineraryVm> GetItinerariesByAirportsOfPlanet(AirportVm lstAirport, CancellationToken cancellationToken = default);
}