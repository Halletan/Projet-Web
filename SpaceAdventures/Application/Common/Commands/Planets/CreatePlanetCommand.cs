using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Queries.Planets.GetPlanet;


namespace SpaceAdventures.Application.Common.Commands.Planets
{
    public record CreatePlanetCommand(string Name) : IRequest<PlanetVm>;

    public class CreatePlanetCommandHandler : IRequestHandler<CreatePlanetCommand, PlanetVm>
    {
        private readonly ISpaceAdventureDbContext _context;
        private readonly IMapper _mapper;


        public CreatePlanetCommandHandler(ISpaceAdventureDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PlanetVm> Handle(CreatePlanetCommand command, CancellationToken cancellationToken)
        {
            var entity = new Planet
            {
                Name = command.Name
            };

            _context.Planets.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            var planetVm = _mapper.Map<Planet, PlanetVm>(entity);
            return planetVm;
        }
    }
}
