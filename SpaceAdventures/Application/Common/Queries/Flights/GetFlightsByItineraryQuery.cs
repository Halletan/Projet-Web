using MediatR;
using SpaceAdventures.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Queries.Flights
{
    public record GetFlightsByItineraryQuery(int id) : IRequest<FlightsVm>;

    public class GetFlightsByItineraryQueryHandler : IRequestHandler<GetFlightsByItineraryQuery, FlightsVm>
    {
        private readonly IFlightService _flightService;

        public GetFlightsByItineraryQueryHandler(IFlightService flightService)
        {
            _flightService = flightService;
        }

        public async Task<FlightsVm> Handle(GetFlightsByItineraryQuery request, CancellationToken cancellationToken)
        {
            return await _flightService.GetFlightsByItinerary(request.id, cancellationToken);
        }
    }
}
