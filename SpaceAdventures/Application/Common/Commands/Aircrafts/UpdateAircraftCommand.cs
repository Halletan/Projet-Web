using MediatR;
using SpaceAdventures.Application.Common.Queries.Aircrafts;
using SpaceAdventures.Application.Common.Services.Interfaces;


namespace SpaceAdventures.Application.Common.Commands.Aircrafts
{
    public record UpdateAircraftCommand(int Id, AircraftInput AircraftInput) : IRequest<AircraftDto>;


    public class UpdateAircraftCommandHandler : IRequestHandler<UpdateAircraftCommand, AircraftDto>
    {

        private readonly IAircraftService _AircraftService;

        public UpdateAircraftCommandHandler(IAircraftService AircraftService)
        {
            _AircraftService = AircraftService;
        }

        public async Task<AircraftDto> Handle(UpdateAircraftCommand request, CancellationToken cancellationToken)
        {
            return await _AircraftService.UpdateAircraft(request.Id, request.AircraftInput, cancellationToken);
        }
    }
}
