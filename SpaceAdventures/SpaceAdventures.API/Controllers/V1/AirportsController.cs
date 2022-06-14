using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaceAdventures.Application.Common.Commands.Airports;
using SpaceAdventures.Application.Common.Queries.Airports;

namespace SpaceAdventures.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AirportsController : ControllerBase
{
    
    private readonly IMediator _mediator;

    /// <summary>
    ///     Airports Controller Constructor
    /// </summary>
    /// <param name="mediator"></param>
    
    public AirportsController(IMediator mediator)
    {
        _mediator = mediator;
       
    }

    /// <summary>
    ///     Get a list of all Airports
    /// </summary>
    //[HttpGet]
    [HttpGet]
    [Authorize(Policy = "read:messages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AirportVm>> GetAirports()
    {
        return Ok(await _mediator.Send(new GetAirportsQuery()));
    }

    /// <summary>
    ///     Get a sepicif Airport by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("GetAirportById/{id}")]
    [Authorize(Policy = "read:messages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AirportDto>> GetAirportById(int id)
    {
        return Ok(await _mediator.Send(new GetAirportByIdQuery(id)));
    }

    /// <summary>
    ///     Create a new Airport
    /// </summary>
    /// <param name="command"></param>
    [HttpPost]
    [Route("Create")]
    [Authorize(Policy = "write:messages")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AirportDto>> CreateAirport([FromBody] CreateAirportCommand command)
    {
        return Ok(await _mediator.Send(command));
    }


    /// <summary>
    ///     Update an existing Airport
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("Update")]
    [Authorize(Policy = "write:messages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AirportDto>> UpdateAirport([FromBody] UpdateAirportCommand command)
    {
        return Ok(await _mediator.Send(command));
    }


    /// <summary>
    ///     Delete an existing Airport
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("Delete")]
    [Authorize(Policy = "write:messages")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteAirport([FromBody] DeleteAirportCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}