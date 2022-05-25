using MediatR;
using SpaceAdventures.Application.Common.Queries.Bookings;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Commands.Bookings;

public record CreateBookingCommand(BookingInput bookingInput) : IRequest<BookingDto>;

public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, BookingDto>
{
    private readonly IBookingService _bookingService;

    public CreateBookingCommandHandler(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    public async Task<BookingDto> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        return await _bookingService.CreateBooking(request.bookingInput, cancellationToken);
    }
}