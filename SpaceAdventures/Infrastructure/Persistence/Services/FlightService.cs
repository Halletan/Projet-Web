using AutoMapper;
using AutoMapper.QueryableExtensions;
using SpaceAdventures.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SpaceAdventures.Application.Common.Commands.Flights;
using SpaceAdventures.Application.Common.Exceptions;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Queries.Flights;

namespace SpaceAdventures.Infrastructure.Persistence.Services;

public class FlightService : IFlightService
{
    private readonly ISpaceAdventureDbContext _context;
    private readonly IMapper _mapper;

    public FlightService(ISpaceAdventureDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<FlightsVm> GetAllFlights(CancellationToken cancellationToken)
    {
        return new FlightsVm
        {
            FlightsList = await _context.Flights
                .ProjectTo<FlightDto>(_mapper.ConfigurationProvider)
                .OrderBy(a => a.IdFlight)
                .ToListAsync(cancellationToken)
        };
    }

    public async Task<FlightDto> GetFlightById(int flightId, CancellationToken cancellation = default)
    {
        var flight = await _context.Flights.FindAsync(flightId);
        if (flight == null) throw new NotFoundException("Flight", flightId);

        return _mapper.Map<FlightDto>(flight);
    }

    public async Task<FlightDto> CreateFlight(FlightInput flightInput, CancellationToken cancellation = default)
    {
        var Flight = _mapper.Map<Flight>(flightInput);

        try
        {
            await _context.Flights.AddAsync(Flight, cancellation);
            await _context.SaveChangesAsync(cancellation);
            return _mapper.Map<FlightDto>(Flight);
        }
        catch (Exception)
        {
            throw new ValidationException();
        }
    }

    public async Task<FlightDto> UpdateFlight(int flightId, FlightInput flightInput,
        CancellationToken cancellation = default)
    {
        var flight = await _context.Flights.FindAsync(flightId);

        if (flight == null) throw new NotFoundException("Flight", flightId);

        try
        {
            flight.IdFlight = flightInput.IdFlight;
            flight.FlightStatus = flightInput.FlightStatus;
            flight.IdAircraft = flightInput.IdAircraft;
            flight.Price = flightInput.Price;
            flight.IdItinerary = flightInput.IdItinerary;

            _context.Flights.Update(flight);
            await _context.SaveChangesAsync(cancellation);
            return _mapper.Map<FlightDto>(flight);
        }
        catch (Exception)
        {
            throw new ValidationException();
        }
    }

    public async Task DeleteFlight(int flightId, CancellationToken cancellation = default)
    {
        var flight = await _context.Flights.FindAsync(flightId);

        if (flight == null) throw new NotFoundException("Flight", flightId);
        _context.Flights.Remove(flight);
    }

    public bool AircraftExists(int id)
    {
        return _context.Aircraft.Any(a => a.IdAircraft == id);
    }

    public bool ItineraryExists(int id)
    {
        return _context.Itineraries.Any(i => i.IdItinerary == id);
    }
}