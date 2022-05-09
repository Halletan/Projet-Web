using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Services.Interfaces;
using MediatR;
using SpaceAdventures.Application.Common.Commands.Airports;
using SpaceAdventures.Application.Common.Queries.Clients;
using SpaceAdventures.Application.Common.Queries.Itineraries;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Commands.Itineraries
{
    public record UpdateItineraryCommand(int Id, ItineraryInput itineraryInput) : IRequest<ItineraryDto>;


    public class UpdateItineraryCommandHandler : IRequestHandler<UpdateItineraryCommand, ItineraryDto>
    {

        private readonly IItineraryService _itineraryService;

        public UpdateItineraryCommandHandler(IItineraryService itineraryService)
        {
            _itineraryService = itineraryService;
        }

        public async Task<ItineraryDto> Handle(UpdateItineraryCommand request, CancellationToken cancellationToken)
        {
            return await _itineraryService.UpdateItinerary(request.Id, request.itineraryInput, cancellationToken);
        }
    }
}
