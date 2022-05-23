using MediatR;
using SpaceAdventures.Application.Common.Queries.AircraftSeats;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Commands.AircraftSeats;

public record UpdateAircraftSeatCommand(int Id, AircraftSeatInput aircraftSeatInput) : IRequest<AircraftSeatDto>;

public class UpdateAircraftSeatCommandHandler : IRequestHandler<UpdateAircraftSeatCommand, AircraftSeatDto>
{
    private readonly IAircraftSeatService _aircraftSeatService;

    public UpdateAircraftSeatCommandHandler(IAircraftSeatService aircraftSeatService)
    {
        _aircraftSeatService = aircraftSeatService;
    }

    public async Task<AircraftSeatDto> Handle(UpdateAircraftSeatCommand request, CancellationToken cancellationToken)
    {
        return await _aircraftSeatService.UpdateAircraftSeat(request.Id, request.aircraftSeatInput, cancellationToken);
    }
}