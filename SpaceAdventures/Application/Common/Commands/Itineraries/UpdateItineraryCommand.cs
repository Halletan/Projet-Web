using MediatR;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Queries.Itineraries;

namespace SpaceAdventures.Application.Common.Commands.Itineraries;

public record UpdateItineraryCommand(int Id, ItineraryInput itineraryInput) : IRequest<ItineraryDto>;

public class UpdateItineraryCommandHandler : IRequestHandler<UpdateItineraryCommand, ItineraryDto>
{
    private readonly IItineraryService _itineraryService;

    public UpdateItineraryCommandHandler(IItineraryService itineraryService)
    {
        _itineraryService = itineraryService;
    }

    public async Task<ItineraryDto> Handle(UpdateItineraryCommand request, CancellationToken cancellationToken)
    {
        return await _itineraryService.UpdateItinerary(request.Id, request.itineraryInput, cancellationToken);
    }
}