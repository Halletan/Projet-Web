using MediatR;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Queries.Itineraries;

public record GetItineraryByIdQuery(int Id) : IRequest<ItineraryDto>;

public class GetItineraryByIdQueryHandler : IRequestHandler<GetItineraryByIdQuery, ItineraryDto>
{
    private readonly IItineraryService _itineraryService;

    public GetItineraryByIdQueryHandler(IItineraryService itineraryService)
    {
        _itineraryService = itineraryService;
    }

    public async Task<ItineraryDto> Handle(GetItineraryByIdQuery request, CancellationToken cancellationToken)
    {
        return await _itineraryService.GetItineraryById(request.Id, cancellationToken);
    }
}