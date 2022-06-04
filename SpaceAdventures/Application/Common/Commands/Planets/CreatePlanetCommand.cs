using MediatR;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Queries.Planets;

namespace SpaceAdventures.Application.Common.Commands.Planets;

public record CreatePlanetCommand(PlanetInput planetInput) : IRequest<PlanetDto>;

public class CreatePlanetCommandHandler : IRequestHandler<CreatePlanetCommand, PlanetDto>
{
    private readonly IPlanetService _planetService;


    public CreatePlanetCommandHandler(IPlanetService planetService)
    {
        _planetService = planetService;
    }

    public async Task<PlanetDto> Handle(CreatePlanetCommand command, CancellationToken cancellationToken)
    {
        return await _planetService.CreatePlanet(command.planetInput, cancellationToken);
    }
}