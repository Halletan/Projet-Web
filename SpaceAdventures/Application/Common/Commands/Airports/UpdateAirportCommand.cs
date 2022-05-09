using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Services.Interfaces;
using Domain.Entities;
using MediatR;
using SpaceAdventures.Application.Common.Queries.Airports;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Commands.Airports
{
    public record UpdateAirportCommand(int Id, AirportInput airportInput) : IRequest<AirportDto>;


    public class UpdateAirportCommandHandler : IRequestHandler<UpdateAirportCommand, AirportDto>
    {

        private readonly IAirportService _airportService;

        public UpdateAirportCommandHandler(IAirportService airportService)
        {
            _airportService = airportService;
        }

        public async Task<AirportDto> Handle(UpdateAirportCommand request, CancellationToken cancellationToken)
        {
            return await _airportService.UpdateAirport(request.Id, request.airportInput, cancellationToken);
        }
    }
}
        
