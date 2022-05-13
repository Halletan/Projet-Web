
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaceAdventures.Application.Common.Commands.Clients;
using SpaceAdventures.Application.Common.Queries.Clients;


namespace SpaceAdventures.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IMediator _mediator;
        
 

        /// <summary>
        /// Clients Controller Constructor
        /// </summary>
        /// <param name="mediator"></param>
        public ClientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all clients
        /// </summary>
        [HttpGet]
        [Authorize(Policy = "read:messages")]
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
        [Route("GetById")]
        public async Task<ActionResult<ClientDto>> GetClientById(int id)
        {
            return await _mediator.Send(new GetclientByIdQuery(id));
        }

        /// <summary>
        /// Create a new client
        /// </summary>
        /// <param name="command"></param>
        [HttpPost]
        [Route("Create")]
        public async Task<ClientDto> CreateClient([FromBody] CreateClientCommand command)
        {
            return await _mediator.Send(command);
        }


        /// <summary>
        /// Update an existing client
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update")]
        public async Task<ClientDto> UpdateClient([FromBody] UpdateClientCommand command)
        {
            return await _mediator.Send(command);
        }


        /// <summary>
        /// Delete an existing client
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task DeleteClient([FromBody] DeleteClientCommand command)
        {
            await _mediator.Send(command);
        }

    }
}
