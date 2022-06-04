using MediatR;
using SpaceAdventures.Application.Common.Interfaces;

namespace SpaceAdventures.Application.Common.Commands.Planets;

public record DeletePlanetCommand(int Id) : IRequest;

public class DeleteClientCommandHandler : IRequestHandler<DeletePlanetCommand>
{
    private readonly IPlanetService _planetService;

    public DeleteClientCommandHandler(IPlanetService planetService)
    {
        _planetService = planetService;
    }

    public async Task<Unit> Handle(DeletePlanetCommand request, CancellationToken cancellationToken)
    {
        await _planetService.DeletePlanet(request.Id, cancellationToken);
        return Unit.Value;
    }
}