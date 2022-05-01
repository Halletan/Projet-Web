using MediatR;
using Microsoft.AspNetCore.Mvc;
using SpaceAdventures.Application.Common.Queries.Clients;

namespace SpaceAdventures.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public Task<IEnumerable<ClientVm>> GetClients()
        {
            return _mediator.Send(new GetClientsQuery());
        }
    }
}
