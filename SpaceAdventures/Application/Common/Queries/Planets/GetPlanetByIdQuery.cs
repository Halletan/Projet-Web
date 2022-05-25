using MediatR;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Queries.Planets;

public record GetPlanetByIdQuery(int Id) : IRequest<PlanetDto>;

public class GetPlanetByIdCommandHandler : IRequestHandler<GetPlanetByIdQuery, PlanetDto>
{
    private readonly IPlanetService _planetService;

    public GetPlanetByIdCommandHandler(IPlanetService planetService)
    {
        _planetService = planetService;
    }

    public async Task<PlanetDto> Handle(GetPlanetByIdQuery request, CancellationToken cancellationToken)
    {
        return await _planetService.GetPlanetById(request.Id, cancellationToken);
    }
}