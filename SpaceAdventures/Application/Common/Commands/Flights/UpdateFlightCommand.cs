using MediatR;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Queries.Flights;

namespace SpaceAdventures.Application.Common.Commands.Flights;

public record UpdateFlightCommand(FlightInput flightInput) : IRequest<FlightDto>;

public class UpdateFlightCommandHandler : IRequestHandler<UpdateFlightCommand, FlightDto>
{
    private readonly IFlightService _flightService;

    public UpdateFlightCommandHandler(IFlightService flightService)
    {
        _flightService = flightService;
    }

    public async Task<FlightDto> Handle(UpdateFlightCommand request, CancellationToken cancellationToken)
    {
        return await _flightService.UpdateFlight(request.flightInput.IdFlight, request.flightInput, cancellationToken);
    }
}