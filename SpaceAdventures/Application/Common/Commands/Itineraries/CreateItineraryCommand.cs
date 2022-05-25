using MediatR;
using SpaceAdventures.Application.Common.Queries.Itineraries;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Commands.Itineraries;

public record CreateItineraryCommand(ItineraryInput itineraryInput) : IRequest<ItineraryDto>;

public class CreateItineraryCommandHandler : IRequestHandler<CreateItineraryCommand, ItineraryDto>
{
    private readonly IItineraryService _itineraryService;

    public CreateItineraryCommandHandler(IItineraryService itineraryService)
    {
        _itineraryService = itineraryService;
    }

    public async Task<ItineraryDto> Handle(CreateItineraryCommand request, CancellationToken cancellationToken)
    {
        return await _itineraryService.CreateItinerary(request.itineraryInput, cancellationToken);
    }
}