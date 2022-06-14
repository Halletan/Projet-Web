using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaceAdventures.Application.Common.Commands.Aircrafts;
using SpaceAdventures.Application.Common.Queries.Aircrafts;

namespace SpaceAdventures.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AircraftsController : ControllerBase
{
    
    private readonly IMediator _mediator;

    /// <summary>
    ///     Aircrafts Controller Constructor
    /// </summary>
    /// <param name="mediator"></param>
   
    public AircraftsController(IMediator mediator)
    {
        _mediator = mediator;
       
    }

    /// <summary>
    ///     Get a list of all Aircrafts
    /// </summary>
    [HttpGet]
    [Authorize(Policy = "read:messages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AircraftsVm>> GetAircrafts()
    {
        return Ok(await _mediator.Send(new GetAircraftsQuery()));
    }

    /// <summary>
    ///     Get a sepicif Aircraft by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("GetById")]
    [Authorize(Policy = "read:messages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AircraftDto>> GetAircraftById(int id)
    {
        return Ok(await _mediator.Send(new GetAircraftByIdQuery(id)));
    }

    /// <summary>
    ///     Create a new Aircraft
    /// </summary>
    /// <param name="command"></param>
    [HttpPost]
    [Route("Create")]
    [Authorize(Policy = "write:messages")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AircraftDto>> CreateAircraft([FromBody] CreateAircraftCommand command)
    {
        return Ok(await _mediator.Send(command));
    }


    /// <summary>
    ///     Update an existing Aircraft
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("Update")]
    [Authorize(Policy = "write:messages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AircraftDto>> UpdateAircraft([FromBody] UpdateAircraftCommand command)
    {
        return Ok(await _mediator.Send(command));
    }


    /// <summary>
    ///     Delete an existing Aircraft
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("Delete")]
    [Authorize(Policy = "write:messages")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteAircraft([FromBody] DeleteAircraftCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}