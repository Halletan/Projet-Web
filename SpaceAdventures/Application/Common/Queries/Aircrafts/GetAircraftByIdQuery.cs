using MediatR;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Queries.Aircrafts;

public record GetAircraftByIdQuery(int Id) : IRequest<AircraftDto>
{
}

public class GetAircraftByIdQueryHandler : IRequestHandler<GetAircraftByIdQuery, AircraftDto>
{
    private readonly IAircraftService _aircraftService;

    public GetAircraftByIdQueryHandler(IAircraftService aircraftService)
    {
        _aircraftService = aircraftService;
    }

    public async Task<AircraftDto> Handle(GetAircraftByIdQuery request, CancellationToken cancellationToken)
    {
        return await _aircraftService.GetAircraftById(request.Id, cancellationToken);
    }
}