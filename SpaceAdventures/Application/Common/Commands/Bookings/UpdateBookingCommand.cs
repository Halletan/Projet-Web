using MediatR;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Queries.Bookings;

namespace SpaceAdventures.Application.Common.Commands.Bookings;

public record UpdateBookingCommand(BookingInput bookingInput) : IRequest<BookingDto>;

public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand, BookingDto>
{
    private readonly IBookingService _bookingService;

    public UpdateBookingCommandHandler(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    public async Task<BookingDto> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
    {
        return await _bookingService.UpdateBooking(request.bookingInput.IdBooking, request.bookingInput, cancellationToken);
    }
}