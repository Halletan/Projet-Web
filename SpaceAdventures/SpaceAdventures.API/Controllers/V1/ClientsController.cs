﻿
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SpaceAdventures.Application.Common.Commands.Clients;
using SpaceAdventures.Application.Common.Models;
using SpaceAdventures.Application.Common.Queries.Clients;
using SpaceAdventures.Application.Common.Queries.Clients.GetClientsWithPagination;

namespace SpaceAdventures.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ClientsController> _logger;

        /// <summary>
        /// Clients Controller Constructor
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="logger"></param>
        public ClientsController(IMediator mediator, ILogger<ClientsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Get all clients
        /// </summary>
        //[HttpGet]
        [HttpGet]
        [Authorize(Policy = "read:messages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ClientDto> GetClientById(int id)
        {
            return await _mediator.Send(new GetclientByIdQuery(id));
        }

        /// <summary>
        /// Create a new client
        /// </summary>
        /// <param name="command"></param>
        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task DeleteClient([FromBody] DeleteClientCommand command)
        {
            await _mediator.Send(command);
        }

    }
}
