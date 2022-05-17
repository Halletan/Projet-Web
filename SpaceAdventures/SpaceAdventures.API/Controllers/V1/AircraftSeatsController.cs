using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaceAdventures.Application.Common.Commands.AircraftSeats;
using SpaceAdventures.Application.Common.Queries.AircraftSeats;

namespace SpaceAdventures.API.Controllers.V1
{

    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AircraftSeatsController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly ILogger<AircraftSeatsController> _logger;

        /// <summary>
        /// AircraftSeats Controller Constructor
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="logger"></param>
        public AircraftSeatsController(IMediator mediator, ILogger<AircraftSeatsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Get a list of all AircraftSeats
        /// </summary>
        //[HttpGet]
        [HttpGet]
        [Authorize(Policy = "read:messages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AircraftSeatsVm>> GetAircraftSeats()
        {

            return Ok(await _mediator.Send(new GetAircraftSeatsQuery()));
        }

        /// <summary>
        /// Get a sepicif AircraftSeat by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById")]
        [Authorize(Policy = "read:messages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AircraftSeatDto>> GetAircraftSeatById(int id)
        {
            return Ok(await _mediator.Send(new GetAircraftSeatByIdQuery(id)));
        }

        /// <summary>
        /// Create a new AircraftSeat
        /// </summary>
        /// <param name="command"></param>
        [HttpPost]
        [Route("Create")]
        [Authorize(Policy = "write:messages")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AircraftSeatDto>> CreateAircraftSeat([FromBody] CreateAircraftSeatCommand command)
        {
            return Ok(await _mediator.Send(command));
        }


        /// <summary>
        /// Update an existing AircraftSeat
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update")]
        [Authorize(Policy = "write:messages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AircraftSeatDto>> UpdateAircraftSeat([FromBody] UpdateAircraftSeatCommand command)
        {
            return Ok(await _mediator.Send(command));
        }


        /// <summary>
        /// Delete an existing AircraftSeat
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete")]
        [Authorize(Policy = "write:messages")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult>DeleteAircraftSeat([FromBody] DeleteAircraftSeatCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

    }
}
