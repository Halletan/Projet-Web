using MediatR;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Queries.Airports;

public record GetAirportByIdQuery(int Id) : IRequest<AirportDto>;

public class GetAirportByIdQueryHandler : IRequestHandler<GetAirportByIdQuery, AirportDto>
{
    private readonly IAirportService _airportService;

    public GetAirportByIdQueryHandler(IAirportService airportService)
    {
        _airportService = airportService;
    }

    public async Task<AirportDto> Handle(GetAirportByIdQuery request, CancellationToken cancellationToken)
    {
        return await _airportService.GetAirportById(request.Id, cancellationToken);
    }
}