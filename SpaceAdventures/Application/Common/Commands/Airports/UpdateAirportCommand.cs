using MediatR;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Queries.Airports;

namespace SpaceAdventures.Application.Common.Commands.Airports;

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