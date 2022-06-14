using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaceAdventures.Application.Common.Commands.Itineraries;
using SpaceAdventures.Application.Common.Queries.Itineraries;

namespace SpaceAdventures.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ItinerariesController : ControllerBase
{
    
    private readonly IMediator _mediator;

    /// <summary>
    ///     Itineraries Controller Constructor
    /// </summary>
    /// <param name="mediator"></param>
    
    public ItinerariesController(IMediator mediator)
    {
        _mediator = mediator;      
    }

    /// <summary>
    ///     Get a list of every Itineraries
    /// </summary>
    [HttpGet]
    [Authorize(Policy = "read:messages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ItineraryVm>> GetItineraries()
    {
        return Ok(await _mediator.Send(new GetItineraryQuery()));
    }

    /// <summary>
    ///     Get a specific Itinerary by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("GetItineraryById/{id}")]
    [Authorize(Policy = "read:messages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ItineraryDto>> GetItineraryById(int id)
    {
        return Ok(await _mediator.Send(new GetItineraryByIdQuery(id)));
    }

    /// <summary>
    ///     Get all Itineraries for a specific destination planet name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("GetItinerariesByDestinationPlanet/{planet}")]
    [Authorize(Policy = "read:messages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ItineraryVm>> GetItinerariesByDestinationPlanet(string planet)
    {
        return Ok(await _mediator.Send(new GetItinerariesByDestinationPlanetQuery(planet)));
    }   

    #region Not used

    /// <summary>
    ///     Create a new Itinerary
    /// </summary>
    /// <param name="command"></param>
    [HttpPost]
    [Route("Create")]
    [Authorize(Policy = "write:messages")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ItineraryDto>> CreateItinerary([FromBody] ItineraryInput itineraryInput)
    {
        CreateItineraryCommand command = new CreateItineraryCommand(itineraryInput);
        return Ok(await _mediator.Send(command));
    }


    /// <summary>
    ///     Update an existing Itinerary
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("Update")]
    [Authorize(Policy = "write:messages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ItineraryDto>> UpdateItinerary([FromBody] ItineraryInput itineraryInput)
    {
        UpdateItineraryCommand command = new UpdateItineraryCommand(itineraryInput);
        return Ok(await _mediator.Send(command));
    }


    /// <summary>
    ///     Delete an existing Itinerary
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("Delete")]
    [Authorize(Policy = "write:messages")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteItinerary([FromBody] DeleteItineraryCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    #endregion
}