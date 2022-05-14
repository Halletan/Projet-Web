using MediatR;
using SpaceAdventures.Application.Common.Queries.Bookings;
using SpaceAdventures.Application.Common.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Commands.Bookings
{
    public record UpdateBookingCommand(int Id, BookingInput bookingInput) : IRequest<BookingDto>;


    public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand, BookingDto>
    {

        private readonly IBookingService _bookingService;

        public UpdateBookingCommandHandler(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task<BookingDto> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
        {
            return await _bookingService.UpdateBooking(request.Id, request.bookingInput, cancellationToken);
        }
    }
}
