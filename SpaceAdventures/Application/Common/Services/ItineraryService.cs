using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SpaceAdventures.Application.Common.Commands.Itinerary;
using SpaceAdventures.Application.Common.Exceptions;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Queries.Itineraries;
using SpaceAdventures.Application.Common.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace SpaceAdventures.Application.Common.Services
{
    internal class ItineraryService : IItineraryService
    {
        private readonly ISpaceAdventureDbContext _context;
        private readonly IMapper _mapper;

        public ItineraryService(ISpaceAdventureDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            var Itinerary = await _context.Itineraries.FindAsync(itineraryId);
            if (Itinerary == null)
            {
                throw new NotFoundException("Itinerary", itineraryId);
            }

            return _mapper.Map<ItineraryDto>(Itinerary);
        }

        public async Task<ItineraryDto> CreateItinerary(ItineraryInput ItineraryInput, CancellationToken cancellation = default)
        {
            var Itinerary = _mapper.Map<Itinerary>(ItineraryInput);

            try
            {
                await _context.Itineraries.AddAsync(Itinerary, cancellation);
                await _context.SaveChangesAsync(cancellation);
                return _mapper.Map<ItineraryDto>(Itinerary);
            }
            catch (Exception)
            {
                throw new ValidationException();
            }
        }

        public async Task<ItineraryDto> UpdateItinerary(int ItineraryId, ItineraryInput ItineraryInput, CancellationToken cancellation = default)
        {
            var Itinerary = await _context.Itineraries.FindAsync(ItineraryId);

            if (Itinerary == null)
            {
                throw new NotFoundException("Itinerary", ItineraryId);
            }

            try
            {
                Itinerary.Rate = ItineraryInput.Rate;
                Itinerary.IdAirport1=ItineraryInput.IdAirport1;
                Itinerary.IdAirport2 = ItineraryInput.IdAirport2;

                _context.Itineraries.Update(Itinerary);
                await _context.SaveChangesAsync(cancellation);
                return _mapper.Map<ItineraryDto>(Itinerary);
            }
            catch (Exception)
            {
                throw new ValidationException();
            }
        }

        public async Task DeleteItinerary(int ItineraryId, CancellationToken cancellation = default)
        {
            var Itinerary = await _context.Itineraries.FindAsync(ItineraryId);

            if (Itinerary == null)
            {
                throw new NotFoundException("Itinerary", ItineraryId);
            }
            _context.Itineraries.Remove(Itinerary);
            await _context.SaveChangesAsync(cancellation);
        }

        public async Task<bool> ItineraryExists(ItineraryInput itineraryInput)
        {
            return await _context.Itineraries.AnyAsync(c => c.IdAirport1== itineraryInput.IdAirport1 && c.IdAirport2== itineraryInput.IdAirport2);
            
        }

    }
}
