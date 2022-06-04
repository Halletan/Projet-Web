using AutoMapper;
using AutoMapper.QueryableExtensions;
using SpaceAdventures.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SpaceAdventures.Application.Common.Commands.Planets;
using SpaceAdventures.Application.Common.Exceptions;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Queries.Planets;

namespace SpaceAdventures.Infrastructure.Persistence.Services;

public class PlanetService : IPlanetService
{
    private readonly ISpaceAdventureDbContext _context;
    private readonly IMapper _mapper;

    public PlanetService(ISpaceAdventureDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PlanetVm> GetAllPlanets(CancellationToken cancellationToken)
    {
        return new PlanetVm
        {
            PlanetsList = await _context.Planets
                .ProjectTo<PlanetDto>(_mapper.ConfigurationProvider)
                .OrderBy(c => c.Name)
                .ToListAsync(cancellationToken)
        };
    }

    public async Task<PlanetDto> GetPlanetById(int planetId, CancellationToken cancellation = default)
    {
        var planet = await _context.Planets.FindAsync(planetId);
        if (planet == null) throw new NotFoundException("planet", planetId);

        return _mapper.Map<PlanetDto>(planet);
    }

    public async Task<PlanetDto> CreatePlanet(PlanetInput planetInput, CancellationToken cancellation = default)
    {
        var planet = _mapper.Map<Planet>(planetInput);

        try
        {
            await _context.Planets.AddAsync(planet, cancellation);
            await _context.SaveChangesAsync(cancellation);
            return _mapper.Map<PlanetDto>(planet);
        }
        catch (Exception)
        {
            throw new ValidationException();
        }
    }

    public async Task<PlanetDto> UpdatePlanet(int planetId, PlanetInput planetInput,
        CancellationToken cancellation = default)
    {
        var planet = await _context.Planets.FindAsync(planetId);

        if (planet == null) throw new NotFoundException("planet", planetId);

        try
        {
            planet.Name = planetInput.Name;
            planet.IdPlanet = planetInput.IdPlanet;

            _context.Planets.Update(planet);
            await _context.SaveChangesAsync(cancellation);
            return _mapper.Map<PlanetDto>(planet);
        }
        catch (Exception)
        {
            throw new ValidationException();
        }
    }

    public async Task DeletePlanet(int planetId, CancellationToken cancellation = default)
    {
        var planet = await _context.Planets.FindAsync(planetId);

        if (planet == null) throw new NotFoundException("planet", planetId);
        _context.Planets.Remove(planet);
        await _context.SaveChangesAsync(cancellation);
    }

    public async Task<bool> PlanetExists(string name)
    {
        return await _context.Planets.AnyAsync(c => c.Name == name);
    }

    public bool PlanetExists(int id, PlanetInput planetInput)
    {
        return _context.Planets.Any(c => c.IdPlanet != id && c.Name == planetInput.Name);
    }
}