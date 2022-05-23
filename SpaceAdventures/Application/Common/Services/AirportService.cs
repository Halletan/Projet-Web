using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SpaceAdventures.Application.Common.Commands.Airports;
using SpaceAdventures.Application.Common.Exceptions;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Queries.Airports;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Services;

public class AirportService : IAirportService
{
    private readonly ISpaceAdventureDbContext _context;
    private readonly IMapper _mapper;

    public AirportService(ISpaceAdventureDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AirportVm> GetAllAirports(CancellationToken cancellationToken)
    {
        return new AirportVm
        {
            AirportsList = await _context.Airports
                .ProjectTo<AirportDto>(_mapper.ConfigurationProvider)
                .OrderBy(c => c.IdPlanet)
                .ToListAsync(cancellationToken)
        };
    }

    public async Task<AirportDto> GetAirportById(int airportId, CancellationToken cancellation = default)
    {
        var airport = await _context.Airports.FindAsync(airportId);
        if (airport == null) throw new NotFoundException("Airport", airportId);

        return _mapper.Map<AirportDto>(airport);
    }

    public async Task<AirportDto> CreateAirport(AirportInput airportInput, CancellationToken cancellation = default)
    {
        var airport = _mapper.Map<Airport>(airportInput);

        try
        {
            await _context.Airports.AddAsync(airport, cancellation);
            await _context.SaveChangesAsync(cancellation);
            return _mapper.Map<AirportDto>(airport);
        }
        catch (Exception)
        {
            throw new ValidationException();
        }
    }

    public async Task<AirportDto> UpdateAirport(int airportId, AirportInput airportInput,
        CancellationToken cancellation = default)
    {
        var airport = await _context.Airports.FindAsync(airportId);

        if (airport == null) throw new NotFoundException("Airport", airportId);

        try
        {
            airport.IdPlanet = airportInput.IdPlanet;
            airport.Name = airportInput.Name;
            airport.IdAirport = airportInput.IdAirport;

            _context.Airports.Update(airport);
            await _context.SaveChangesAsync(cancellation);
            return _mapper.Map<AirportDto>(airport);
        }
        catch (Exception)
        {
            throw new ValidationException();
        }
    }

    public async Task DeleteAirport(int airportId, CancellationToken cancellation = default)
    {
        var airport = await _context.Airports.FindAsync(airportId);

        if (airport == null) throw new NotFoundException("Airport", airportId);
        _context.Airports.Remove(airport);
        await _context.SaveChangesAsync(cancellation);
    }

    public async Task<bool> AirportExists(string name, int planetId)
    {
        return await _context.Airports.AnyAsync(c => c.Name == name && c.IdPlanet == planetId);
    }

    public bool AirportExists(int? id, AirportInput airportInput)
    {
        return _context.Airports.Any(c => c.IdAirport != id && c.Name == airportInput.Name);
    }

    public bool PlanetExists(int id)
    {
        return _context.Planets.Any(c => c.IdPlanet == id);
    }
}