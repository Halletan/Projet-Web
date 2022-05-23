using MediatR;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Queries.Itineraries;

// Query
public class GetItineraryQuery : IRequest<ItineraryVm>
{
}

// Handler
public class GetItineraryQueryHandler : IRequestHandler<GetItineraryQuery, ItineraryVm>
{
    private readonly IItineraryService _ItineraryService;

    public GetItineraryQueryHandler(IItineraryService ItineraryService)
    {
        _ItineraryService = ItineraryService;
    }

    public async Task<ItineraryVm> Handle(GetItineraryQuery request, CancellationToken cancellationToken)
    {
        return await _ItineraryService.GetAllItineraries(cancellationToken);
    }
}