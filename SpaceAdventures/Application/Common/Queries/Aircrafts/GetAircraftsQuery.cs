using MediatR;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Queries.Aircrafts;

public class GetAircraftsQuery : IRequest<AircraftsVm>
{
}

public class GetAircraftsQueryHandler : IRequestHandler<GetAircraftsQuery, AircraftsVm>
{
    private readonly IAircraftService _aircraftService;

    public GetAircraftsQueryHandler(IAircraftService aircraftService)
    {
        _aircraftService = aircraftService;
    }

    public async Task<AircraftsVm> Handle(GetAircraftsQuery request, CancellationToken cancellationToken)
    {
        return await _aircraftService.GetAllAircrafts(cancellationToken);
    }
}