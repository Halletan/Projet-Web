using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaceAdventures.Application.Common.Commands.Flights;
using SpaceAdventures.Application.Common.Queries.Flights;

namespace SpaceAdventures.API.Controllers.V1
{

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FlightsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<FlightsController> _logger;

        /// <summary>
        /// Flights Controller Constructor
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="logger"></param>
        public FlightsController(IMediator mediator, ILogger<FlightsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Get a list of all Flights
        /// </summary>
        //[HttpGet]
        [HttpGet]
        [Authorize(Policy = "read:messages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<FlightsVm> GetFlights()
        {

            return await _mediator.Send(new GetFlightsQuery());
        }

        /// <summary>
        /// Get a sepicif Flight by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<FlightDto> GetFlightById(int id)
        {
            return await _mediator.Send(new GetFlightByIdQuery(id));
        }

        /// <summary>
        /// Create a new Flight
        /// </summary>
        /// <param name="command"></param>
        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<FlightDto> CreateFlight([FromBody] CreateFlightCommand command)
        {
            return await _mediator.Send(command);
        }


        /// <summary>
        /// Update an existing Flight
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<FlightDto> UpdateFlight([FromBody] UpdateFlightCommand command)
        {
            return await _mediator.Send(command);
        }


        /// <summary>
        /// Delete an existing Flight
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task DeleteFlight([FromBody] DeleteFlightCommand command)
        {
            await _mediator.Send(command);
        }
    }
}
