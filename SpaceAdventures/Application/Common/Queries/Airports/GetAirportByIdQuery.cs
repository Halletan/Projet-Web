using MediatR;
using SpaceAdventures.Application.Common.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Queries.Airports
{
    public record GetAirportByIdQuery(int Id) : IRequest<AirportDto>;

    public class GetClientByIdQueryHandler : IRequestHandler<GetAirportByIdQuery, AirportDto>
    {
        private readonly IAirportService _airportService;

        public GetClientByIdQueryHandler(IAirportService airportService)
        {
            _airportService = airportService;
        }

        public async Task<AirportDto> Handle(GetAirportByIdQuery request, CancellationToken cancellationToken)
        {
            return await _airportService.GetAirportById(request.Id, cancellationToken);
        }
    }
}
