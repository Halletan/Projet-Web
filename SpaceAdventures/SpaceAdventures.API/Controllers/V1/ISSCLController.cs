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
    public class ISSCLController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ISSCLController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get the International Space Station Current Location
        /// </summary>
        /// <returns>The position of the space station</returns>
        [HttpGet]
        // [Authorize]
        public async Task<ActionResult<ISSCLPosition>> GetPostion()
        {
            return Ok(await _mediator.Send(new ISSCLQuery()));
        }
    }
}
