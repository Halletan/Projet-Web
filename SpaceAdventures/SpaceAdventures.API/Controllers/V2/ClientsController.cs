using MediatR;
using Microsoft.AspNetCore.Mvc;
using SpaceAdventures.Application.Common.Queries.Clients;
using SpaceAdventures.Application.Common.Queries.Planets;


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
        public Task<ClientsVm> GetClients()
        {
            return _mediator.Send(new GetClientsQuery());
        }

        [HttpGet]
        [Route("Planets")]
        public Task<PlanetVm> GetPlanets()
        {
            return _mediator.Send(new GetPlanetsQuery());
        }

    }
}
