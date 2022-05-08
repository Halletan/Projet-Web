
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SpaceAdventures.Application.Common.Commands.Clients;
using SpaceAdventures.Application.Common.Models;
using SpaceAdventures.Application.Common.Queries.Clients;
using SpaceAdventures.Application.Common.Queries.Clients.GetClientsWithPagination;
using SpaceAdventures.Application.Common.Queries.Planets.GetPlanet;

namespace SpaceAdventures.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all clients
        /// </summary>
        [HttpGet]
        public async Task<ClientsVm> GetClients()
        {
            return await _mediator.Send(new GetClientsQuery());
        }

        /// <summary>
        /// Get client by its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetClientById")]
        public async Task<ClientDto> GetClientById(int id)
        {
            return await _mediator.Send(new GetClientByIdQuery(id));
        }

        /// <summary>
        /// Get all client with pagination
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ClientWithPagination")]
        public async Task<ActionResult<PaginatedList<ClientsBriefDto>>> GetClientsWithPagination(
            [FromQuery] GetClientsWithPaginationQuery query)
        {
            return await _mediator.Send(query);
        }

        /// <summary>
        /// Create a new client
        /// </summary>
        /// <param name="command"></param>
        [HttpPost]
        [Route("CreateClient")]
        public async Task<ClientDto> CreateClient([FromBody] CreateClientCommand command)
        {
            return await _mediator.Send(command);
        }

    }
}
