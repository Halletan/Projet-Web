using MediatR;
using SpaceAdventures.Application.Common.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Queries.AircraftSeats
{
    // Query
    public class GetAircraftSeatsQuery : IRequest<AircraftSeatsVm> { }

    // Handler
    public class GetAircraftSeatsQueryHandler : IRequestHandler<GetAircraftSeatsQuery, AircraftSeatsVm>
    {
        private readonly IAircraftSeatService _aircraftSeatService;

        public GetAircraftSeatsQueryHandler(IAircraftSeatService aircraftSeatService)
        {
            _aircraftSeatService = aircraftSeatService;
        }

        public async Task<AircraftSeatsVm> Handle(GetAircraftSeatsQuery request, CancellationToken cancellationToken)
        {
            return await _aircraftSeatService.GetAllAircraftSeats(cancellationToken);
        }
    }
}
