using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SpaceAdventures.Application.Common.Commands.Aircrafts;
using SpaceAdventures.Application.Common.Exceptions;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Queries.Aircrafts;

namespace SpaceAdventures.Infrastructure.Persistence.Services;

public class AircraftService : IAircraftService
{
    private readonly ISpaceAdventureDbContext _context;
    private readonly IMapper _mapper;

    public AircraftService(ISpaceAdventureDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AircraftsVm> GetAllAircrafts(CancellationToken cancellationToken)
    {
        return new AircraftsVm
        {
            AircraftsList = await _context.Aircraft
                .ProjectTo<AircraftDto>(_mapper.ConfigurationProvider)
                .OrderBy(a => a.IdAircraft)
                .ToListAsync(cancellationToken)
        };
    }

    public async Task<AircraftDto> GetAircraftById(int aircraftId, CancellationToken cancellation = default)
    {
        var aircraft = await _context.Aircraft.FindAsync(aircraftId);
        if (aircraft == null) throw new NotFoundException("Aircraft", aircraftId);

        return _mapper.Map<AircraftDto>(aircraft);
    }

    public async Task<AircraftDto> CreateAircraft(AircraftInput aircraftInput, CancellationToken cancellation = default)
    {
        var aircraft = _mapper.Map<Aircraft>(aircraftInput);

        try
        {
            await _context.Aircraft.AddAsync(aircraft, cancellation);
            await _context.SaveChangesAsync(cancellation);
            return _mapper.Map<AircraftDto>(aircraft);
        }
        catch (Exception)
        {
            throw new ValidationException();
        }
    }

    public async Task<AircraftDto> UpdateAircraft(int aircraftId, AircraftInput aircraftInput,
        CancellationToken cancellation = default)
    {
        var aircraft = await _context.Aircraft.FindAsync(aircraftId);

        if (aircraft == null) throw new NotFoundException("Aircraft", aircraftId);

        try
        {
            aircraft.IdAircraft = aircraftInput.IdAircraft;
            aircraft.Manufacturer = aircraftInput.Manufacturer;
            aircraft.Model = aircraftInput.Model;
            aircraft.NumberOfSeats = aircraftInput.NumberOfSeats;


            _context.Aircraft.Update(aircraft);
            await _context.SaveChangesAsync(cancellation);
            return _mapper.Map<AircraftDto>(aircraft);
        }
        catch (Exception)
        {
            throw new ValidationException();
        }
    }


    public async Task DeleteAircraft(int aircraftId, CancellationToken cancellation = default)
    {
        var aircraft = await _context.Aircraft.FindAsync(aircraftId);

        if (aircraft == null) throw new NotFoundException("Aircraft", aircraftId);
        _context.Aircraft.Remove(aircraft);
    }

    //public bool AircraftExists(int? id, AircraftInput aircraftInput)
    //{
    //    return _context.Aircraft.Any(c => c.IdAircraft == id);
    //}
}