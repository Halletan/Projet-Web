
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Services.Interfaces;
using MediatR;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Commands.Itinerary
{
    public record DeleteItineraryCommand(int Id) : IRequest;

    public class DeleteItineraryCommandHandler : IRequestHandler<DeleteItineraryCommand>
    {
        private readonly IItineraryService _itineraryService;

        public DeleteItineraryCommandHandler(IItineraryService itineraryService)
        {
            _itineraryService = itineraryService;
        }

        public async Task<Unit> Handle(DeleteItineraryCommand request, CancellationToken cancellationToken)
        {
            await _itineraryService.DeleteItinerary(request.Id, cancellationToken);
            return Unit.Value;
        }
    }
}
