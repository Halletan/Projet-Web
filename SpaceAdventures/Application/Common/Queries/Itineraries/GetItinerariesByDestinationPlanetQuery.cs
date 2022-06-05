using MediatR;
using SpaceAdventures.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Queries.Itineraries
{
    public record GetItinerariesByDestinationPlanetQuery(string name) : IRequest<ItineraryVm>;
     

    public class GetItinerariesByDestinationPlanetQueryHandler : IRequestHandler<GetItinerariesByDestinationPlanetQuery, ItineraryVm>
    {
        private readonly IItineraryService _itineraryService;
        private readonly IPlanetService _planetService;
        private readonly IAirportService _airportService;

        public GetItinerariesByDestinationPlanetQueryHandler(IItineraryService itineraryService, IPlanetService planetService, IAirportService airportService)
        {
            _itineraryService = itineraryService;
            _planetService = planetService;
            _airportService = airportService;
        }

        public async Task<ItineraryVm> Handle(GetItinerariesByDestinationPlanetQuery request, CancellationToken cancellationToken)
        {
            var planet = await _planetService.GetPlanetByName(request.name, cancellationToken);
            var airportsList = await _airportService.GetAirportsByDestination(planet.IdPlanet, cancellationToken);
            

            return await _itineraryService.GetItinerariesByAirportsOfPlanet(airportsList, cancellationToken);
        }
    }
}
