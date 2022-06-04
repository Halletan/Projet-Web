using MediatR;
using SpaceAdventures.Application.Common.Interfaces;

namespace SpaceAdventures.Application.Common.Queries.AircraftSeats;

// Query
public class GetAircraftSeatsQuery : IRequest<AircraftSeatsVm>
{
}

// Handler
public class GetAircraftSeatsQueryHandler : IRequestHandler<GetAircraftSeatsQuery, AircraftSeatsVm>
{
    private readonly IAircraftSeatService _aircraftSeatService;

    public GetAircraftSeatsQueryHandler(IAircraftSeatService aircraftSeatService)
    {
        _aircraftSeatService = aircraftSeatService;
    }

    public async Task<AircraftSeatsVm> Handle(GetAircraftSeatsQuery request, CancellationToken cancellationToken)
    {
        return await _aircraftSeatService.GetAllAircraftSeats(cancellationToken);
    }
}