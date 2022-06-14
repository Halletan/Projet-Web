using MediatR;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Queries.Planets;

namespace SpaceAdventures.Application.Common.Commands.Planets;

public record UpdatePlanetCommand(PlanetInput planetInput) : IRequest<PlanetDto>;

public class UpdateClientCommandHandler : IRequestHandler<UpdatePlanetCommand, PlanetDto>
{
    private readonly IPlanetService _planetService;

    public UpdateClientCommandHandler(IPlanetService planetService)
    {
        _planetService = planetService;
    }

    public async Task<PlanetDto> Handle(UpdatePlanetCommand request, CancellationToken cancellationToken)
    {
        return await _planetService.UpdatePlanet(request.planetInput.IdPlanet, request.planetInput, cancellationToken);
    }
}