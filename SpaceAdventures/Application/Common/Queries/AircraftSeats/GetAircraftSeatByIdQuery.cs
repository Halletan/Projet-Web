using MediatR;
using SpaceAdventures.Application.Common.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Queries.AircraftSeats
{
    public record GetAircraftSeatByIdQuery(int Id) : IRequest<AircraftSeatDto>;

    public class GetAircraftSeatByIdQueryHandler : IRequestHandler<GetAircraftSeatByIdQuery, AircraftSeatDto>
    {
        private readonly IAircraftSeatService _aircraftSeatService;

        public GetAircraftSeatByIdQueryHandler(IAircraftSeatService aircraftSeatService)
        {
            _aircraftSeatService = aircraftSeatService;
        }

        public async Task<AircraftSeatDto> Handle(GetAircraftSeatByIdQuery request, CancellationToken cancellationToken)
        {
            return await _aircraftSeatService.GetAircraftSeatById(request.Id, cancellationToken);
        }
    }
}
