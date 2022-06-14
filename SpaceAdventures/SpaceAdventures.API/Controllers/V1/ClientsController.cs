using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaceAdventures.Application.Common.Commands.Clients;
using SpaceAdventures.Application.Common.Queries.Clients;

namespace SpaceAdventures.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ClientsController> _logger;
    /// <summary>
    ///     Clients Controller Constructor
    /// </summary>
    /// <param name="mediator"></param>
    public ClientsController(IMediator mediator, ILogger<ClientsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    ///     Get all clients
    /// </summary>
    [HttpGet]
    [Authorize(Policy = "read:messages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClientsVm>> GetClients()
    {
        return Ok(await _mediator.Send(new GetClientsQuery()));
    }

    /// <summary>
    ///     Get client by its Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("GetById/{id}")]
    [Authorize(Policy = "read:messages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClientDto>> GetClientById(int id)
    {
        return Ok(await _mediator.Send(new GetClientByIdQuery(id)));
    }

    /// <summary>
    ///     Get client by Email
    /// </summary>
    /// <param name="Email"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("GetClientByEmail/{Email}")]
    [Authorize(Policy = "read:messages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClientDto>> GetClientByEmail(string Email)
    {
        return Ok(await _mediator.Send(new GetClientByEmailQuery(Email)));
    }

    

    /// <summary>
    ///     Create a new client
    /// </summary>
    /// <param name="command"></param>
    [HttpPost]
    [Route("Create")]
    [Authorize(Policy = "write:messages")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ClientDto>> CreateClient([FromBody] ClientInput clientInput)
    {
        CreateClientCommand command = new CreateClientCommand(clientInput);
        return Ok(await _mediator.Send(command));
    }


    /// <summary>
    ///     Update an existing client
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("Update")]
    [Authorize(Policy = "write:messages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClientDto>> UpdateClient([FromBody] ClientInput clientInput)
    {
        UpdateClientCommand command = new UpdateClientCommand(clientInput);
        return Ok(await _mediator.Send(command));
    }


    /// <summary>
    ///     Delete an existing client
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("Delete")]
    [Authorize(Policy = "write:messages")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteClient([FromBody] DeleteClientCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}