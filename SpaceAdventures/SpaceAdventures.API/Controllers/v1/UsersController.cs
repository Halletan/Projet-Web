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

    #region User


    ///  <summary>
    ///     Get a user by its Id
    /// </summary>
    /// <param name="id"></param>
    [HttpPost]
    [Authorize(Policy = "read:users")]
    [Route("GetUserById/{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<UserDto>> GetUserById(int id)
    {
       
        return Ok(await _mediator.Send(GetUserByIdQuery(id));
    }
    

    /// <summary>
    ///     Create a new user
    /// </summary>
    /// <param name="command"></param>
    [HttpPost]
    [Authorize(Policy = "write:users")]
    [Route("CreateUser")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<UserDto>> CreateUser([FromBody] UserInput input)
    {
        CreateUserCommand command = new CreateUserCommand(input);
        return Ok(await _mediator.Send(command));
    }

    /// <summary>
    ///     New user sign up
    /// </summary>
    /// <param name="command"></param>
    [HttpPost]
    //[Authorize(Policy = "write:users")]
    [Route("CreateUserSignUp")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<UserDto>> CreateUserSignUp([FromBody] UserInput input)
    {
        CreateUserCommand command = new CreateUserCommand(input);
        return Ok(await _mediator.Send(command));
    }



    /// <summary>
    ///     Update a user
    /// </summary>
    /// <param name="command"></param>
    [HttpPatch]
    [Authorize(Policy = "write:users")]
    [Route("UpdateUser/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<UserDto>> UpdateUser(int id, [FromBody] UserInput userInput)
    {
        UpdateUserCommand command = new UpdateUserCommand(id, userInput);
        return Ok(await _mediator.Send(command));
    }

    /// <summary>
    ///     Get List of users from DB
    /// </summary>
    /// <returns>List of users</returns>
    [HttpGet]
    [Authorize(Policy = "read:messages")]
    [Route("GetAllUsers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UsersVm>> GetAllUsers()
    {
        return await _mediator.Send(new GetUsersQuery());
    }


    /// <summary>
    ///     Get Lis of users from Auth0 Platform
    /// </summary>
    /// <returns>List of users</returns>
    [HttpGet]
    [Authorize(Policy = "read:messages")]
    [Route("GetAllAuth0Users")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<UserAuth0>>> GetAllAuth0Users() 
    {
        return await _mediator.Send(new GetAllAuth0Users());
    }



    /// <summary>
    ///     Get a user from DB
    /// </summary>
    /// <returns>a user</returns>
    [HttpGet]
    [Authorize(Policy = "read:messages")]
    [Route("GetUserByEmail/{email}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> GetUserByEmail(string email)
    {
        return await _mediator.Send(new GetUserByEmailQuery(email));
    }


    /// <summary>
    ///     delete user
    /// </summary>
    /// <returns> </returns>
    [HttpDelete]
    [Authorize(Policy = "write:users")]
    [Route("DeleteUser/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteUser(int id)
    {
        DeleteUserCommand command = new DeleteUserCommand(id);

        await _mediator.Send(command);
        return NoContent();
    }
    #endregion

    #region Role

    /// <summary>
    ///     Get a user's roles
    /// </summary>
    /// <param name="id"></param>
    /// <returns>List of user's roles</returns>
    [HttpGet]
    [Authorize(Policy = "read:messages")]
    [Route("UserRoles/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<UserRole>>> GetUserRoles(string id)
    {
        return await _mediator.Send(new GetUserRolesQuery(id));
    }


    /// <summary>
    ///     Get all role in DB
    /// </summary>
    /// <returns>List of roles</returns>
    [HttpGet]
    [Authorize(Policy = "read:messages")]
    [Route("GetAllRole")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RolesVm>> GetAllRole()
    {
        return await _mediator.Send(new GetAllRoleQuery());
    }

    /// <summary>
    ///     Get all role in DB
    /// </summary>
    /// <param name="id"></param>
    /// <returns>a role with the matching id</returns>
    [HttpGet]
    [Authorize(Policy = "read:messages")]
    [Route("GetRoleByIdRole/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RoleDto>> GetRoleByIdRole(int id)
    {
        return Ok(await _mediator.Send(new GetRoleByIdRoleQuery(id)));
    }

    /// <summary>
    ///     Assign roles to a specific user
    /// </summary>
    /// <param name="command"></param>
    [HttpPost]
    [Authorize(Policy = "write:users")]
    [Route("AssignRoles")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> CreateUser([FromQuery] AssignRolesCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    #endregion














}