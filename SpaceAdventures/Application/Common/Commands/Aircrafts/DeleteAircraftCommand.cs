using MediatR;
using SpaceAdventures.Application.Common.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Commands.Aircrafts
{
    public record DeleteAircraftCommand(int Id) : IRequest;

    public class DeleteAircraftCommandHandler : IRequestHandler<DeleteAircraftCommand>
    {
        private readonly IAircraftService _AircraftService;

        public DeleteAircraftCommandHandler(IAircraftService AircraftService)
        {
            _AircraftService = AircraftService;
        }

        public async Task<Unit> Handle(DeleteAircraftCommand request, CancellationToken cancellationToken)
        {
            await _AircraftService.DeleteAircraft(request.Id, cancellationToken);
            return Unit.Value;
        }
    }
}
