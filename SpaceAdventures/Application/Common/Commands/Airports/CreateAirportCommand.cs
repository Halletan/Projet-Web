using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Services.Interfaces;
using MediatR;
using SpaceAdventures.Application.Common.Queries.Airports;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Commands.Airports
{
    public record CreateAirportCommand(AirportInput AirportInput) : IRequest<AirportDto>;

    public class CreateAirportCommandHandler : IRequestHandler<CreateAirportCommand, AirportDto>
    {
        private readonly IAirportService _AirportService;

        public CreateAirportCommandHandler(IAirportService AirportService)
        {
            _AirportService = AirportService;
        }

        public async Task<AirportDto> Handle(CreateAirportCommand request, CancellationToken cancellationToken)
        {
             return await _AirportService.CreateAirport(request.AirportInput, cancellationToken);
        }
    }
}
    