using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpaceAdventures.Application.Common.Commands.Membership;
using SpaceAdventures.Application.Common.Queries.Membership;

namespace SpaceAdventures.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MembershipController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<MembershipController> _logger;

        /// <summary>
        /// Membership Controller Constructor
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="logger"></param>
        public MembershipController(IMediator mediator, ILogger<MembershipController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Get a list of every Membership 
        /// </summary>
        //[HttpGet]
        [HttpGet]
        [Authorize(Policy = "read:messages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MembershipVm>> GetMembership()
        {

            return Ok(await _mediator.Send(new GetMembershipQuery()));
        }

        /// <summary>
        /// Get a specific Membership by an id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById")]
        [Authorize(Policy = "read:messages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MembershipDto>> GetMembershipById(int id)
        {
            return Ok(await _mediator.Send(new GetMembershipByIdQuery(id)));
        }

        /// <summary>
        /// Create a new Membership
        /// </summary>
        /// <param name="command"></param>
        [HttpPost]
        [Route("Create")]
        [Authorize(Policy = "write:messages")]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public async Task<ActionResult<MembershipDto>> CreateMembership([FromBody] CreateMembershipCommand command)
        {
            return Ok(await _mediator.Send(command));
        }


        /// <summary>
        /// Update an existing Membership
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update")]
        [Authorize(Policy = "write:messages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MembershipDto>> UpdateMembership([FromBody] UpdateMembershipCommand command)
        {
            return Ok(await _mediator.Send(command));
        }


        /// <summary>
        /// Delete an existing Membership
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete")]
        [Authorize(Policy = "write:messages")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult>DeleteMembership([FromBody] DeleteMembershipCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
