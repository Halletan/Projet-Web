using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SpaceAdventures.Application.Common.Commands.Itineraries;
using SpaceAdventures.Application.Common.Exceptions;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Queries.Airports;
using SpaceAdventures.Application.Common.Queries.Itineraries;
using SpaceAdventures.Domain.Entities;

namespace SpaceAdventures.Infrastructure.Persistence.Services;

public class ItineraryService : IItineraryService
{
    private readonly ISpaceAdventureDbContext _context;
    private readonly IMapper _mapper;
    private readonly IPlanetService _planetService;

    public ItineraryService(ISpaceAdventureDbContext context, IMapper mapper, IPlanetService planetService)
    {
        _context = context;
        _mapper = mapper;
        _planetService = planetService;
    }


    public bool ItineraryExists(ItineraryInput itineraryInput)
    {
        return _context.Itineraries.Any(c =>
            c.IdAirport1 == itineraryInput.IdAirport1 && c.IdAirport2 == itineraryInput.IdAirport2);
    }

    public async Task<ItineraryVm> GetAllItineraries(CancellationToken cancellationToken)
    {
        return new ItineraryVm
        {
            ItinerariesList = await _context.Itineraries
                .ProjectTo<ItineraryDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
        };
    }

    public async Task<ItineraryDto> GetItineraryById(int itineraryId, CancellationToken cancellation = default)
    {
        var itinerary = await _context.Itineraries.FindAsync(itineraryId);
        if (itinerary == null) throw new NotFoundException("Itinerary", itineraryId);

        return _mapper.Map<ItineraryDto>(itinerary);
    }

    public async Task<ItineraryVm> GetItinerariesByAirportsOfPlanet(/* List<Airport>*/ AirportVm lstAirport, CancellationToken cancellationToken = default)
    {

        List<int> airportsId = lstAirport.AirportsList.Select(a => a.IdAirport).ToList();
        List<int> allDestinationAirports = _context.Itineraries.Select(i => i.IdAirport2).ToList();

        var test = _context.Itineraries.Where(i => airportsId.Contains(i.IdAirport2)).ToList();

        //IEnumerable<Itinerary> itinerariesListByAirpot = _context.Itineraries.Where(i => i.IdAirport2 == airport.IdAirport).ToList();

        //itinerariesLst.AddRange(itinerariesListByAirpot);

        //var itinerariesLst = _context.Itineraries.Join(airportsId, i => i.IdAirport2, a => a, (i, a) => new Itinerary
        //{
        //    IdItinerary = i.IdItinerary,
        //    IdAirport1 = i.IdAirport1,
        //    IdAirport2 = i.IdAirport2
        //});

        return new ItineraryVm
        {
            ItinerariesList = await _context.Itineraries.Where(i => airportsId.Contains(i.IdAirport2))
                .ProjectTo<ItineraryDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
        };

        //ItineraryVm vm = new ItineraryVm();
        //if (itinerariesLst != null)
        //{
        //    foreach (Itinerary itinerary in itinerariesLst)
        //    {
        //        vm.ItinerariesList.Add(_mapper.Map<ItineraryDto>(itinerary));
        //    }


        //}

        //return vm;


    }


    //public async Task<ItineraryVm> GetItinerariesByDestinationPlanet(, CancellationToken cancellationToken = default)
    //{

    //    List<Itinerary> itinerariesTempList = null;


    //    // List of Itineraries where Airport(s) of destination is on this planet.
    //    if (airportsList != null)
    //    {
    //        foreach (Airport airport in airportsList)
    //        {
    //            IEnumerable<Itinerary> itinerariesListByAirpot = _context.Itineraries.Where(i => i.IdAirport2 == airport.IdAirport).ToList();

    //            itinerariesTempList.AddRange(itinerariesListByAirpot);
    //        }
    //    }

    //    // List ItineraryVm => List of ItinerariesDTO.
    //    ItineraryVm vm = new ItineraryVm();
    //    if (itinerariesTempList != null)
    //    {

    //        foreach (Itinerary itinerary in itinerariesTempList)
    //        {
    //            vm.ItinerariesList.Add(_mapper.Map<ItineraryDto>(itinerary));
    //        }

    //        return vm;
    //    }

    //    return null;

    //}

    #region Not used
    public async Task<ItineraryDto> CreateItinerary(ItineraryInput itineraryInput,
        CancellationToken cancellation = default)
    {
        var itinerary = _mapper.Map<Itinerary>(itineraryInput);

        try
        {
            await _context.Itineraries.AddAsync(itinerary, cancellation);
            await _context.SaveChangesAsync(cancellation);
            return _mapper.Map<ItineraryDto>(itinerary);
        }
        catch (Exception)
        {
            throw new ValidationException();
        }
    }

    public async Task<ItineraryDto> UpdateItinerary(int itineraryId, ItineraryInput itineraryInput,
        CancellationToken cancellation = default)
    {
        var itinerary = await _context.Itineraries.FindAsync(itineraryId);

        if (itinerary == null) throw new NotFoundException("Itinerary", itineraryId);

        try
        {

            itinerary.IdAirport1 = itineraryInput.IdAirport1;
            itinerary.IdAirport2 = itineraryInput.IdAirport2;

            _context.Itineraries.Update(itinerary);
            await _context.SaveChangesAsync(cancellation);
            return _mapper.Map<ItineraryDto>(itinerary);
        }
        catch (Exception)
        {
            throw new ValidationException();
        }
    }

    public async Task DeleteItinerary(int itineraryId, CancellationToken cancellation = default)
    {
        var itinerary = await _context.Itineraries.FindAsync(itineraryId);

        if (itinerary == null) throw new NotFoundException("Itinerary", itineraryId);
        _context.Itineraries.Remove(itinerary);
        await _context.SaveChangesAsync(cancellation);
    }
    #endregion

}
