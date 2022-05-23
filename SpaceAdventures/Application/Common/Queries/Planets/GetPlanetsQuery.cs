using MediatR;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Queries.Planets;

public record GetPlanetsQuery : IRequest<PlanetVm>;

public class GetPlanetsQueryHandler : IRequestHandler<GetPlanetsQuery, PlanetVm>
{
    private readonly IPlanetService _planetService;

    public GetPlanetsQueryHandler(IPlanetService planetService)
    {
        _planetService = planetService;
    }

    public async Task<PlanetVm> Handle(GetPlanetsQuery request, CancellationToken cancellationToken)
    {
        return await _planetService.GetAllPlanets(cancellationToken);
    }
}