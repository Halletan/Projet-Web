using MediatR;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Commands.Airports;

public record DeleteAirportCommand(int Id) : IRequest;

public class DeleteAirportCommandHandler : IRequestHandler<DeleteAirportCommand>
{
    private readonly IAirportService _AirportService;

    public DeleteAirportCommandHandler(IAirportService AirportService)
    {
        _AirportService = AirportService;
    }

    public async Task<Unit> Handle(DeleteAirportCommand request, CancellationToken cancellationToken)
    {
        await _AirportService.DeleteAirport(request.Id, cancellationToken);
        return Unit.Value;
    }
}