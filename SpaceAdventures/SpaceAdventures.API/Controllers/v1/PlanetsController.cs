using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaceAdventures.Application.Common.Commands.Planets;
using SpaceAdventures.Application.Common.Queries.Planets;

namespace SpaceAdventures.API.Controllers.V1
{

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PlanetsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PlanetsController> _logger;

        /// <summary>
        /// Planet Controller Constructor
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="logger"></param>
        public PlanetsController(IMediator mediator, ILogger<PlanetsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Get a list of every planet 
        /// </summary>
        //[HttpGet]
        [HttpGet]
        [Authorize(Policy = "read:messages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<PlanetVm> GetPlanets()
        {
            return await _mediator.Send(new GetPlanetsQuery());
        }

        /// <summary>
        /// Get a specific planet by an id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<PlanetDto> GetPlanetById(int id)
        {
            return await _mediator.Send(new GetPlanetByIdQuery(id));
        }

        /// <summary>
        /// Create a new planet
        /// </summary>
        /// <param name="command"></param>
        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<PlanetDto> CreatePlanet([FromBody] CreatePlanetCommand command)
        {
            return await _mediator.Send(command);
        }


        /// <summary>
        /// Update an existing planet
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlanetDto>> UpdatePlanet([FromBody] UpdatePlanetCommand command)
        {
            return await _mediator.Send(command);
        }


        /// <summary>
        /// Delete an existing Planet
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task DeletePlanet([FromBody] DeletePlanetCommand command)
        {
            await _mediator.Send(command);
        }
    }
}
