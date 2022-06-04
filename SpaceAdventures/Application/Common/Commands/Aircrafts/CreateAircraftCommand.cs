using MediatR;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Queries.Aircrafts;

namespace SpaceAdventures.Application.Common.Commands.Aircrafts;

public record CreateAircraftCommand(AircraftInput aircraftInput) : IRequest<AircraftDto>;

public class CreateAircraftCommandHandler : IRequestHandler<CreateAircraftCommand, AircraftDto>
{
    private readonly IAircraftService _aircraftService;

    public CreateAircraftCommandHandler(IAircraftService aircraftService)
    {
        _aircraftService = aircraftService;
    }

    public async Task<AircraftDto> Handle(CreateAircraftCommand request, CancellationToken cancellationToken)
    {
        return await _aircraftService.CreateAircraft(request.aircraftInput, cancellationToken);
    }
}