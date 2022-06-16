using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaceAdventures.Application.Common.Models;
using SpaceAdventures.Application.Common.Queries.Bookings;
using SpaceAdventures.Application.Common.Queries.Bookings.GetBookingsWithPagination;
using SpaceAdventures.Application.Common.Queries.Clients;
using SpaceAdventures.Application.Common.Queries.Clients.GetClientsWithPagination;

namespace SpaceAdventures.API.Controllers.V2;

[ApiController]
[ApiVersion("2.0")]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
public class GlobalController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    ///     Clients Controller Constructor
    /// </summary>
    /// <param name="mediator"></param>
    public GlobalController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Get all clients with pagination
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    [Authorize(Policy = "read:messages")]
    [Route("ClientWithPagination")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PaginatedList<ClientDto>>> GetClientsWithPagination(
        [FromQuery] GetClientsWithPaginationQuery query)
    {
        return await _mediator.Send(query);
    }

    /// <summary>
    ///     Get a paginated List of bookings
    /// </summary>
    [HttpGet]
    [Route("GetBookingsWithPagination")]
    [Authorize(Policy = "read:messages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PaginatedList<BookingDto>>> GetBookingsWithPagination(
        [FromQuery] GetBookingsWithPaginationQuery query)
    {
        return await _mediator.Send(query);
    }
}