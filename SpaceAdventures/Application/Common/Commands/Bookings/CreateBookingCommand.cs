using AutoMapper;
using MediatR;
using SpaceAdventures.Application.Common.Commands.Flights;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Queries.Bookings;
using SpaceAdventures.Application.Common.Queries.Planets;
using SpaceAdventures.Domain.Entities;

namespace SpaceAdventures.Application.Common.Commands.Bookings;

public record CreateBookingCommand(BookingInput bookingInput) : IRequest<BookingDto>;

public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, BookingDto>
{
    private readonly IBookingService _bookingService;
    private readonly IFlightService _flightService;
    private readonly IMapper _mapper;

    public CreateBookingCommandHandler(IBookingService bookingService, IFlightService flightService, IMapper mapper)
    {
        _bookingService = bookingService;
        _flightService = flightService;
        _mapper = mapper;
    }

    public async Task<BookingDto> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        var flight = await _flightService.GetFlightById(request.bookingInput.IdFlight);
        flight.RemainingSeats -= request.bookingInput.NbSeats;
        var test = _mapper.Map<Flight>(flight);
        _ = _flightService.UpdateFlight(request.bookingInput.IdFlight, _mapper.Map<FlightInput>(test), cancellationToken);
        return await _bookingService.CreateBooking(request.bookingInput, cancellationToken);
    }
}