using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SpaceAdventures.Application.Common.Commands.AircraftSeats;
using SpaceAdventures.Application.Common.Exceptions;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Queries.AircraftSeats;
using SpaceAdventures.Application.Common.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Services
{
    public class AircraftSeatService : IAircraftSeatService
    {
        private readonly ISpaceAdventureDbContext _context;
        private readonly IMapper _mapper;

        public AircraftSeatService(ISpaceAdventureDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AircraftSeatsVm> GetAllAircraftSeats(CancellationToken cancellationToken)
        {
            return new AircraftSeatsVm
            {
                     AircraftSeatsList = await _context.AircraftSeats
                    .ProjectTo<AircraftSeatDto>(_mapper.ConfigurationProvider)
                    
                    .ToListAsync(cancellationToken)
            };
        }

        public async Task<AircraftSeatDto> GetAircraftSeatById(int aircraftSeatId, CancellationToken cancellation = default)
        {
            var aircraftSeat = await _context.AircraftSeats.FindAsync(aircraftSeatId);
            if (aircraftSeat == null)
            {
                throw new NotFoundException("AircraftSeat", aircraftSeatId);
            }

            return _mapper.Map<AircraftSeatDto>(aircraftSeat);
        }

        public async Task<AircraftSeatDto> CreateAircraftSeat(AircraftSeatInput aircraftSeatInput, CancellationToken cancellation = default)
        {
            var aircraftSeat = _mapper.Map<AircraftSeat>(aircraftSeatInput);

            try
            {
                await _context.AircraftSeats.AddAsync(aircraftSeat, cancellation);
                await _context.SaveChangesAsync(cancellation);
                return _mapper.Map<AircraftSeatDto>(aircraftSeat);
            }
            catch (Exception)
            {
                throw new ValidationException();
            }
        }

        public async Task<AircraftSeatDto> UpdateAircraftSeat(int aircraftSeatId, AircraftSeatInput AircraftSeatInput, CancellationToken cancellation = default)
        {
            var AircraftSeat = await _context.AircraftSeats.FindAsync(aircraftSeatId);

            if (AircraftSeat == null)
            {
                throw new NotFoundException("AircraftSeat", aircraftSeatId);
            }

            try
            {
                
                _context.AircraftSeats.Update(AircraftSeat);
                await _context.SaveChangesAsync(cancellation);
                return _mapper.Map<AircraftSeatDto>(AircraftSeat);
            }
            catch (Exception)
            {
                throw new ValidationException();
            }
        }

        public async Task DeleteAircraftSeat(int AircraftSeatId, CancellationToken cancellation = default)
        {
            var AircraftSeat = await _context.AircraftSeats.FindAsync(AircraftSeatId);

            if (AircraftSeat == null)
            {
                throw new NotFoundException("AircraftSeat", AircraftSeatId);
            }
            _context.AircraftSeats.Remove(AircraftSeat);
            await _context.SaveChangesAsync(cancellation);
        }

        //public async Task<bool> AircraftSeatExists(string email)
        //{
        //    return await _context.AircraftSeats.AnyAsync(c => c.Email == email);
        //}

        //public bool AircraftSeatExists(int? id, AircraftSeatInput AircraftSeatInput)
        //{
        //    return _context.AircraftSeats.Any(c => c.IdAircraftSeat != id && c.Email == AircraftSeatInput.Email);
        //}
    }
}
