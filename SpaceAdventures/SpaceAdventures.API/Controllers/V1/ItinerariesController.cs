using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpaceAdventures.Application.Common.Commands.Itineraries;
using SpaceAdventures.Application.Common.Commands.Planets;
using SpaceAdventures.Application.Common.Queries.Itineraries;
using SpaceAdventures.Application.Common.Queries.Planets;

namespace SpaceAdventures.API.Controllers.V1
{
        [ApiController]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/[controller]")]
        public class ItinerariesController : ControllerBase
        {
            private readonly IMediator _mediator;
            private readonly ILogger<ItinerariesController> _logger;

            /// <summary>
            /// Itineraries Controller Constructor
            /// </summary>
            /// <param name="mediator"></param>
            /// <param name="logger"></param>
            public ItinerariesController(IMediator mediator, ILogger<ItinerariesController> logger)
            {
                _mediator = mediator;
                _logger = logger;
            }

            /// <summary>
            /// Get a list of every Itineraries
            /// </summary>
            [HttpGet]
            [Authorize(Policy = "read:messages")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<ItineraryVm> GetItineraries()
            {

                return await _mediator.Send(new GetItineraryQuery());
            }

            /// <summary>
            /// Get a specific Itinerary by id
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            [HttpGet]
            [Route("GetById")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<ItineraryDto> GetItineraryById(int id)
            {
                return await _mediator.Send(new GetItineraryByIdQuery(id));
            }

            /// <summary>
            /// Create a new itinerary
            /// </summary>
            /// <param name="command"></param>
            [HttpPost]
            [Route("Create")]
            [ProducesResponseType(StatusCodes.Status201Created)]
            public async Task<ItineraryDto> CreatePlanet([FromBody] CreateItineraryCommand command)
            {
                return await _mediator.Send(command);
            }


            /// <summary>
            /// Update an existing itinerary
            /// </summary>
            /// <param name="command"></param>
            /// <returns></returns>
            [HttpPut]
            [Route("Update")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<ItineraryDto> UpdatePlanet([FromBody] UpdateItineraryCommand command)
            {
                return await _mediator.Send(command);
            }


            /// <summary>
            /// Delete an existing Itineraries
            /// </summary>
            /// <param name="command"></param>
            /// <returns></returns>
            [HttpDelete]
            [Route("Delete")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task DeleteItinerary([FromBody] DeleteItineraryCommand command)
            {
                await _mediator.Send(command);
            }
        }
    
}

