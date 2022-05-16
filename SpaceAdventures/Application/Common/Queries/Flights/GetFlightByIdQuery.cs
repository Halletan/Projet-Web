using MediatR;
using SpaceAdventures.Application.Common.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Queries.Flights
{

    public record GetFlightByIdQuery(int Id) : IRequest<FlightDto>;

    public class GetFlightByIdQueryHandler : IRequestHandler<GetFlightByIdQuery, FlightDto>
    {
        private readonly IFlightService _flightService;

        public GetFlightByIdQueryHandler(IFlightService flightService)
        {
            _flightService = flightService;
        }

        public async Task<FlightDto> Handle(GetFlightByIdQuery request, CancellationToken cancellationToken)
        {
            return await _flightService.GetFlightById(request.Id, cancellationToken);
        }
    }

}

