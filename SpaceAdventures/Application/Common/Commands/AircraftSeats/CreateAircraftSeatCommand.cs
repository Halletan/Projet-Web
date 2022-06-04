using MediatR;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Queries.AircraftSeats;

namespace SpaceAdventures.Application.Common.Commands.AircraftSeats;

public record CreateAircraftSeatCommand(AircraftSeatInput aircraftSeatInput) : IRequest<AircraftSeatDto>;

public class CreateAircraftSeatCommandHandler : IRequestHandler<CreateAircraftSeatCommand, AircraftSeatDto>
{
    private readonly IAircraftSeatService _aircraftSeatService;

    public CreateAircraftSeatCommandHandler(IAircraftSeatService aircraftSeatService)
    {
        _aircraftSeatService = aircraftSeatService;
    }

    public async Task<AircraftSeatDto> Handle(CreateAircraftSeatCommand request, CancellationToken cancellationToken)
    {
        return await _aircraftSeatService.CreateAircraftSeat(request.aircraftSeatInput, cancellationToken);
    }
}