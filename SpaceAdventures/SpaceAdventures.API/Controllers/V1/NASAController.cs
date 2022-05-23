using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpaceAdventures.Application.Common.Models.APIConsume;
using SpaceAdventures.Application.Common.Models.UsersAuth0ManagementApi;
using SpaceAdventures.Application.Common.Queries.ISSCL;
using SpaceAdventures.Application.Common.Queries.Users.Queries;

namespace SpaceAdventures.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class NASAController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NASAController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get the Nasa Album of a specific planet
        /// </summary>
        /// <returns>The Nasa's albums</returns>
        [HttpGet]
        [Route("GetAlbum/{search}")]
        public async Task<ActionResult<NasaCollection>> GetAlbum(string search)
        {
            return Ok(await _mediator.Send(new NasaQuery(search)));
        }
    }
}
