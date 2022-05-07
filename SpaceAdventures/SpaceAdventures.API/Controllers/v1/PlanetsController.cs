using MediatR;
using Microsoft.AspNetCore.Mvc;
using SpaceAdventures.Application.Common.Commands.Planets;
using SpaceAdventures.Application.Common.Queries.Planets.GetPlanet;

namespace SpaceAdventures.API.Controllers.V1
{

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PlanetsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PlanetsController (IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("Planets")]
        public Task<PlanetVm> GetPlanets()
        {
            return _mediator.Send(new GetPlanetsQuery());
        }

        [HttpGet]
        [Route("Planets/{id}")]
        public Task<PlanetVm> GetPlanetById(int id)
        {
            return _mediator.Send(new GetPlanetByIdQuery(id));
        }

        [HttpPost]
        [Route("CreatePlanet")]
        public async Task<ActionResult<PlanetVm>> CreatePlanet(CreatePlanetCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
