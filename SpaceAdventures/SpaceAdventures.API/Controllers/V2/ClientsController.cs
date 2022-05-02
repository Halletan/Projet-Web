using MediatR;
using Microsoft.AspNetCore.Mvc;
using SpaceAdventures.Application.Common.Queries.Clients;
using SpaceAdventures.Application.Common.Queries.Planets.GetPlanet;

namespace SpaceAdventures.API.Controllers.V2    
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public Task<IEnumerable<ClientVm>> GetClients()
        {
            return _mediator.Send(new GetClientsQuery());
        }

        [HttpGet]        
        public Task<PlanetVm> GetPlanet()
        {
            return _mediator.Send(new GetPlanetsQuery());
        }

    }
}
