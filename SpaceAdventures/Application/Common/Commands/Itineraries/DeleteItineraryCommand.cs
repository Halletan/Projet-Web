using MediatR;
using SpaceAdventures.Application.Common.Interfaces;

namespace SpaceAdventures.Application.Common.Commands.Itineraries;

public record DeleteItineraryCommand(int Id) : IRequest;

public class DeleteItineraryCommandHandler : IRequestHandler<DeleteItineraryCommand>
{
    private readonly IItineraryService _itineraryService;

    public DeleteItineraryCommandHandler(IItineraryService itineraryService)
    {
        _itineraryService = itineraryService;
    }

    public async Task<Unit> Handle(DeleteItineraryCommand request, CancellationToken cancellationToken)
    {
        await _itineraryService.DeleteItinerary(request.Id, cancellationToken);
        return Unit.Value;
    }
}