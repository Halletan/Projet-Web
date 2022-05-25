using MediatR;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Queries.Flights;

// Query
public class GetFlightsQuery : IRequest<FlightsVm>
{
}

// Handler
public class GetFlightsQueryHandler : IRequestHandler<GetFlightsQuery, FlightsVm>
{
    private readonly IFlightService _flightService;

    public GetFlightsQueryHandler(IFlightService flightService)
    {
        _flightService = flightService;
    }

    public async Task<FlightsVm> Handle(GetFlightsQuery request, CancellationToken cancellationToken)
    {
        return await _flightService.GetAllFlights(cancellationToken);
    }
}