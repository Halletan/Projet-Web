using MediatR;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Commands.Aircrafts;

public record DeleteAircraftCommand(int Id) : IRequest;

public class DeleteAircraftCommandHandler : IRequestHandler<DeleteAircraftCommand>
{
    private readonly IAircraftService _aircraftService;

    public DeleteAircraftCommandHandler(IAircraftService aircraftService)
    {
        _aircraftService = aircraftService;
    }

    public async Task<Unit> Handle(DeleteAircraftCommand request, CancellationToken cancellationToken)
    {
        await _aircraftService.DeleteAircraft(request.Id, cancellationToken);
        return Unit.Value;
    }
}