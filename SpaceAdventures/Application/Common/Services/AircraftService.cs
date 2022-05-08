using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SpaceAdventures.Application.Common.Exceptions;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Queries.Aircrafts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Services
{
    public class AircraftService
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
        if (aircraft == null)
        {
            throw new NotFoundException("Aircraft", aircraftId);
        }

        return _mapper.Map<AircraftDto>(aircraft);
    }

    //public async Task<AircraftDto> CreateAircraft(AircraftInput AircraftInput, CancellationToken cancellation = default)
    //{
    //    var aircraft = _mapper.Map<Aircraft>(AircraftInput);

    //    try
    //    {
    //        await _context.Aircrafts.AddAsync(aircraft, cancellation);
    //        await _context.SaveChangesAsync(cancellation);
    //        return _mapper.Map<AircraftDto>(aircraft);
    //    }
    //    catch (Exception)
    //    {
    //        throw new ValidationException();
    //    }
    //}

    public async Task<AircraftDto> UpdateAircraft(int aircraftId, AircraftDto aircraftDto, CancellationToken cancellation = default)
    {
        if (!await _context.Aircraft.AnyAsync(c => c.IdAircraft == aircraftId, cancellationToken: cancellation))
        {
            throw new NotFoundException("Aircraft", aircraftId);
        }

        try
        {
            _context.Aircraft.Update(_mapper.Map<Aircraft>(aircraftDto));
            await _context.SaveChangesAsync(cancellation);
            return aircraftDto;
        }
        catch (Exception)
        {
            throw new ValidationException();
        }
    }

    public async Task DeleteAircraft(int aircraftId, CancellationToken cancellation = default)
    {
        var aircraft = await _context.Aircraft.FindAsync(aircraftId);

        if (aircraft == null)
        {
            _context.Aircraft.Remove(aircraft);
        }
        throw new NotFoundException("Aircraft", aircraftId);
    }

   
    }
}
