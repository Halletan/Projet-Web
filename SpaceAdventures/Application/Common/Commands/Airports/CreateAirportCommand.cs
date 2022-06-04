using MediatR;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Queries.Airports;

namespace SpaceAdventures.Application.Common.Commands.Airports;

public record CreateAirportCommand(AirportInput AirportInput) : IRequest<AirportDto>;

public class CreateAirportCommandHandler : IRequestHandler<CreateAirportCommand, AirportDto>
{
    private readonly IAirportService _AirportService;

    public CreateAirportCommandHandler(IAirportService AirportService)
    {
        _AirportService = AirportService;
    }

    public async Task<AirportDto> Handle(CreateAirportCommand request, CancellationToken cancellationToken)
    {
        return await _AirportService.CreateAirport(request.AirportInput, cancellationToken);
    }
}