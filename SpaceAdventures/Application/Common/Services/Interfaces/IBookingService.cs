using SpaceAdventures.Application.Common.Commands.Bookings;
using SpaceAdventures.Application.Common.Queries.Bookings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Services.Interfaces
{
    public interface IBookingService
    {
        Task<BookingsVm> GetAllBookings(CancellationToken cancellation = default);
        Task<BookingDto> GetBookingById(int BookingId, CancellationToken cancellation = default);
        Task<BookingDto> CreateBooking(BookingInput BookingInput, CancellationToken cancellation = default);
        Task<BookingDto> UpdateBooking(int BookingId, BookingInput BookingInput, CancellationToken cancellation = default);
        Task DeleteBooking(int BookingId, CancellationToken cancellation = default);

        //bool BookingExists(int? id, BookingInput BookingInput);

        bool FlightExists(int id);
        bool ClientExists(int id);


    }
}
