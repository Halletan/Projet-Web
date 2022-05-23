using MediatR;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Commands.Bookings;

public record DeleteBookingCommand(int Id) : IRequest;

public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand>
{
    private readonly IBookingService _bookingService;

    public DeleteBookingCommandHandler(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    public async Task<Unit> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
    {
        await _bookingService.DeleteBooking(request.Id, cancellationToken);
        return Unit.Value;
    }
}