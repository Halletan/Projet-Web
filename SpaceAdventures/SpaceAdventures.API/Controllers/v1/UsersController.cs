using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaceAdventures.Application.Common.Models.UsersAuth0ManagementApi;
using SpaceAdventures.Application.Common.Queries.Users.Queries;

namespace SpaceAdventures.API.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get a user's roles
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List of user's roles</returns>
        [HttpGet]
        // [Authorize]
        public async Task<ActionResult<Roles>> GetUserRoles(string userId)
        {
            return await _mediator.Send(new GetUserRolesQuery(userId, false));  
        }
    }
}
