using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaceAdventures.Application.Common.Commands.Bookings;
using SpaceAdventures.Application.Common.Queries.Bookings;

namespace SpaceAdventures.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly ILogger<BookingsController> _logger;


    private readonly IMediator _mediator;

    /// <summary>
    ///     Bookings Controller Constructor
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="logger"></param>
    public BookingsController(IMediator mediator, ILogger<BookingsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    ///     Get a list of all Bookings
    /// </summary>
    //[HttpGet]
    [HttpGet]
    [Authorize(Policy = "read:messages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BookingsVm>> GetBookings()
    {
        return Ok(await _mediator.Send(new GetBookingsQuery()));
    }

    /// <summary>
    ///     Get a sepicif Booking by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("GetById")]
    [Authorize(Policy = "read:messages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BookingDto>> GetBookingById(int id)
    {
        return Ok(await _mediator.Send(new GetBookingByIdQuery(id)));
    }

    /// <summary>
    ///     Create a new Booking
    /// </summary>
    /// <param name="command"></param>
    [HttpPost]
    [Route("CreateBooking")]
    [Authorize(Policy = "write:messages")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BookingDto>> CreateBooking([FromBody] BookingInput bookingInput)
    {
        var command = new CreateBookingCommand(bookingInput);
        return Ok(await _mediator.Send(command));
    }


    /// <summary>
    ///     Update an existing Booking
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("Update")]
    [Authorize(Policy = "write:messages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BookingDto>> UpdateBooking([FromBody] UpdateBookingCommand command)
    {
        return Ok(await _mediator.Send(command));
    }


    /// <summary>
    ///     Delete an existing Booking
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("Delete")]
    [Authorize(Policy = "write:messages")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteBooking([FromBody] DeleteBookingCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}