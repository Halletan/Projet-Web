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
        public async Task<MembershipVm> GetMembership()
        {

            return await _mediator.Send(new GetMembershipQuery());
        }

        /// <summary>
        /// Get a specific Membership by an id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<MembershipDto> GetMembershipById(int id)
        {
            return await _mediator.Send(new GetMembershipByIdQuery(id));
        }

        /// <summary>
        /// Create a new Membership
        /// </summary>
        /// <param name="command"></param>
        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<MembershipDto> CreateMembership([FromBody] CreateMembershipCommand command)
        {
            return await _mediator.Send(command);
        }


        /// <summary>
        /// Update an existing Membership
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<MembershipDto> UpdateMembership([FromBody] UpdateMembershipCommand command)
        {
            return await _mediator.Send(command);
        }


        /// <summary>
        /// Delete an existing Membership
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task DeleteMembership([FromBody] DeleteMembershipCommand command)
        {
            await _mediator.Send(command);
        }
    }
}
