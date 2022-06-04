using SpaceAdventures.Application.Common.Commands.Itineraries;
using SpaceAdventures.Application.Common.Queries.Itineraries;

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
}