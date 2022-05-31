using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaceAdventures.Application.Common.Commands.Users;
using SpaceAdventures.Application.Common.Models.UsersAuth0ManagementApi;
using SpaceAdventures.Application.Common.Queries.Users.Queries;

namespace SpaceAdventures.API.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    ///     Users Constructor
    /// </summary>
    /// <param name="mediator"></param>
    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Get a user's roles
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>List of user's roles</returns>
    [HttpGet]
    // [Authorize(Policy = "read:users")]
    [Route("UserRoles")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<UserRole>>> GetUserRoles(string userId)
    {
        return await _mediator.Send(new GetUserRolesQuery(userId));
    }

    /// <summary>
    ///     Create a new user
    /// </summary>
    /// <param name="command"></param>
    [HttpPost]
    //[Authorize(Policy = "write:users")]
    [Route("CreateUser")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<UserDto>> CreateUser([FromBody] UserInput input/*CreateUserCommand command*/)
    {
        CreateUserCommand command = new CreateUserCommand(input);
        return Ok(await _mediator.Send(command));
    }

    /// <summary>
    ///     Get Lis of users from DB
    /// </summary>
    /// <returns>List of users</returns>
    [HttpGet]
    // [Authorize(Policy = "read:users")]
    [Route("GetAllUsers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UsersVm>> GetAllUsers()
    {
        return await _mediator.Send(new GetUsersQuery());
    }


}