using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Services.Interfaces;
using MediatR;
using SpaceAdventures.Application.Common.Queries.Itineraries;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Commands.Itinerary
{
    public record CreateItineraryCommand(ItineraryInput itineraryInput) : IRequest<ItineraryDto>;

    public class CreateClientCommandHandler : IRequestHandler<CreateItineraryCommand, ItineraryDto>
    {
        private readonly IItineraryService _itineraryService;

        public CreateClientCommandHandler(IItineraryService itineraryService)
        {
            _itineraryService = itineraryService;
        }

        public async Task<ItineraryDto> Handle(CreateItineraryCommand request, CancellationToken cancellationToken)
        {
             return await _itineraryService.CreateItinerary(request.itineraryInput, cancellationToken);
        }
    }
}
    