
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaceAdventures.Application.Common.Models;
using SpaceAdventures.Application.Common.Queries.Clients.GetClientsWithPagination;

namespace SpaceAdventures.API.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
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
        /// Get all clients with pagination
        /// </summary>
        /// <param name="query"></param>
        /// <response code="201">Client Created</response>
        [HttpGet]
        [Route("ClientWithPagination")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<PaginatedList<ClientsBriefDto>>> GetClientsWithPagination(
            [FromQuery] GetClientsWithPaginationQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
