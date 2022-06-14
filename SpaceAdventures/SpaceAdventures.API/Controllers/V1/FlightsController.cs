using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaceAdventures.Application.Common.Commands.Flights;
using SpaceAdventures.Application.Common.Queries.Flights;

namespace SpaceAdventures.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
public class FlightsController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    ///     Flights Controller Constructor
    /// </summary>
    /// <param name="mediator"></param>
    
    public FlightsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Get a list of all Flights
    /// </summary>
    //[HttpGet]
    [HttpGet]
    [Authorize(Policy = "read:messages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FlightsVm>> GetFlights()
    {
        return Ok(await _mediator.Send(new GetFlightsQuery()));
    }

    /// <summary>
    ///     Get a specific Flight by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("GetFlightById/{id}")]
    [Authorize(Policy = "read:messages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FlightDto>> GetFlightById(int id)
    {
        return Ok(await _mediator.Send(new GetFlightByIdQuery(id)));
    }

    /// <summary>
    ///     Get list of available flights for a specific Itinerary
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("GetFlightsByItinerary/{id}")]
    [Authorize(Policy = "read:messages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FlightsVm>> GetFlightsByItinerary(int id)
    {
        return Ok(await _mediator.Send(new GetFlightsByItineraryQuery(id)));
    }

    #region Not Used
    /// <summary>
    ///     Create a new Flight
    /// </summary>
    /// <param name="command"></param>
    [HttpPost]
    [Route("Create")]
    [Authorize(Policy = "write:messages")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<FlightDto>> CreateFlight([FromBody] FlightInput flightInput)
    {
        CreateFlightCommand command = new CreateFlightCommand(flightInput);
        return Ok(await _mediator.Send(command));
    }


    /// <summary>
    ///     Update an existing Flight
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("Update")]
    [Authorize(Policy = "write:messages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FlightDto>> UpdateFlight([FromBody] FlightInput flightInput)
    {
        UpdateFlightCommand command = new UpdateFlightCommand(flightInput); 
        return Ok(await _mediator.Send(command));
    }


    /// <summary>
    ///     Delete an existing Flight
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("Delete")]
    [Authorize(Policy = "write:messages")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteFlight([FromBody] DeleteFlightCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
    #endregion
}