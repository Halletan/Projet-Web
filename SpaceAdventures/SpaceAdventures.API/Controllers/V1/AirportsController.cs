using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpaceAdventures.Application.Common.Commands.Airports;
using SpaceAdventures.Application.Common.Commands.Clients;
using SpaceAdventures.Application.Common.Queries.Airports;
using SpaceAdventures.Application.Common.Queries.Clients;

namespace SpaceAdventures.API.Controllers.V1
{
        [ApiController]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/[controller]")]
        public class AirportsController : ControllerBase
        {
            private readonly IMediator _mediator;
            private readonly ILogger<AirportsController> _logger;

            /// <summary>
            /// Airports Controller Constructor
            /// </summary>
            /// <param name="mediator"></param>
            /// <param name="logger"></param>
            public AirportsController(IMediator mediator, ILogger<AirportsController> logger)
            {
                _mediator = mediator;
                _logger = logger;
            }

            /// <summary>
            /// Get a list of all Airports
            /// </summary>
            //[HttpGet]
            [HttpGet]
            [Authorize(Policy = "read:messages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<AirportVm> GetAirports()
            {

                return await _mediator.Send(new GetAirportsQuery());
            }

            /// <summary>
            /// Get a sepicif Airport by id
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            [HttpGet]
            [Route("GetById")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<AirportDto> GetAirportById(int id)
            {
                return await _mediator.Send(new GetAirportByIdQuery(id));
            }

            /// <summary>
            /// Create a new Airport
            /// </summary>
            /// <param name="command"></param>
            [HttpPost]
            [Route("Create")]
            [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<AirportDto> CreateAirport([FromBody] CreateAirportCommand command)
            {
                return await _mediator.Send(command);
            }


            /// <summary>
            /// Update an existing Airport
            /// </summary>
            /// <param name="command"></param>
            /// <returns></returns>
            [HttpPut]
            [Route("Update")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<AirportDto> UpdateAirport([FromBody] UpdateAirportCommand command)
            {
                return await _mediator.Send(command);
            }


            /// <summary>
            /// Delete an existing Airport
            /// </summary>
            /// <param name="command"></param>
            /// <returns></returns>
            [HttpDelete]
            [Route("Delete")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task DeleteAirport([FromBody] DeleteAirportCommand command)
            {
                await _mediator.Send(command);
            }
        }
    
}
