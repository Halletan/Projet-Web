using MediatR;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Queries.Bookings;

public record GetBookingByIdQuery(int Id) : IRequest<BookingDto>;

public class GetBookingByIdQueryHandler : IRequestHandler<GetBookingByIdQuery, BookingDto>
{
    private readonly IBookingService _bookingService;

    public GetBookingByIdQueryHandler(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    public async Task<BookingDto> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
    {
        return await _bookingService.GetBookingById(request.Id, cancellationToken);
    }
}