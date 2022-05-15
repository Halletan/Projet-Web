using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaceAdventures.Application.Common.Commands.Aircrafts;
using SpaceAdventures.Application.Common.Queries.Aircrafts;

namespace SpaceAdventures.API.Controllers.V1
{

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AircraftsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AircraftsController> _logger;

        /// <summary>
        /// Aircrafts Controller Constructor
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="logger"></param>
        public AircraftsController(IMediator mediator, ILogger<AircraftsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Get a list of all Aircrafts
        /// </summary>
        //[HttpGet]
        [HttpGet]
        [Authorize(Policy = "read:messages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<AircraftsVm> GetAircrafts()
        {

            return await _mediator.Send(new GetAircraftsQuery());
        }

        /// <summary>
        /// Get a sepicif Aircraft by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<AircraftDto> GetAircraftById(int id)
        {
            return await _mediator.Send(new GetAircraftByIdQuery(id));
        }

        /// <summary>
        /// Create a new Aircraft
        /// </summary>
        /// <param name="command"></param>
        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<AircraftDto> CreateAircraft([FromBody] CreateAircraftCommand command)
        {
            return await _mediator.Send(command);
        }


        /// <summary>
        /// Update an existing Aircraft
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<AircraftDto> UpdateAircraft([FromBody] UpdateAircraftCommand command)
        {
            return await _mediator.Send(command);
        }


        /// <summary>
        /// Delete an existing Aircraft
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task DeleteAircraft([FromBody] DeleteAircraftCommand command)
        {
            await _mediator.Send(command);
        }
    }

}

