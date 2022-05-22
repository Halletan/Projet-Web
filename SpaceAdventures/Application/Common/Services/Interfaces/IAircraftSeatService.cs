using SpaceAdventures.Application.Common.Commands.AircraftSeats;
using SpaceAdventures.Application.Common.Queries.AircraftSeats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Services.Interfaces
{
    public interface IAircraftSeatService
    {
        Task<AircraftSeatsVm> GetAllAircraftSeats(CancellationToken cancellation = default);
        Task<AircraftSeatDto> GetAircraftSeatById(int AircraftSeatId, CancellationToken cancellation = default);
        Task<AircraftSeatDto> CreateAircraftSeat(AircraftSeatInput AircraftSeatInput, CancellationToken cancellation = default);
        Task<AircraftSeatDto> UpdateAircraftSeat(int AircraftSeatId, AircraftSeatInput AircraftSeatInput, CancellationToken cancellation = default);
        Task DeleteAircraftSeat(int AircraftSeatId, CancellationToken cancellation = default);
       
        //bool AircraftSeatExists(int? id, AircraftSeatInput AircraftSeatInput);
        bool BookingExists(int id);
        bool FlightExists(int id);
    }
}
