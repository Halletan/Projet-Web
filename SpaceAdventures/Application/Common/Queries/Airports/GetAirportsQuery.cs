using MediatR;
using SpaceAdventures.Application.Common.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Queries.Airports
{
    public record GetAirportsQuery : IRequest<AirportVm> { }

    // Handler
    public class GetAirportsQueryHandler : IRequestHandler<GetAirportsQuery, AirportVm>
    {
        private readonly IAirportService _airportService;

        public GetAirportsQueryHandler(IAirportService airportService)
        {
            _airportService = airportService;
        }

        public async Task<AirportVm> Handle(GetAirportsQuery request, CancellationToken cancellationToken)
        {
            return await _airportService.GetAllAirports(cancellationToken);
        }
    }
}
