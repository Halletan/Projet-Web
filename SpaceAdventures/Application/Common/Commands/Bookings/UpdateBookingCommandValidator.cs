using FluentValidation;
using SpaceAdventures.Application.Common.Interfaces;

namespace SpaceAdventures.Application.Common.Commands.Bookings;

public class UpdateBookingCommandValidator : AbstractValidator<UpdateBookingCommand>
{
    public UpdateBookingCommandValidator(IBookingService bookingService)
    {
        RuleFor(b => b.bookingInput.BookingAmount).NotEmpty().WithMessage("Booking amount is mandatory")
            .InclusiveBetween(1, 1000000).WithMessage("Booking amount should be between 1 and 1000000");

        // Control existing IdFlight
        RuleFor(b => b.bookingInput.IdFlight)
            .Must(idflight =>
            {
                var exists = bookingService.FlightExists(idflight);
                return !exists;
            }).WithMessage("This flight doesn't exist !");

        // Control existing IdClient
        RuleFor(b => b.bookingInput.IdClient)
            .Must(idclient =>
            {
                var exists = bookingService.ClientExists(idclient);
                return !exists;
            }).WithMessage("This client doesn't exist !");
    }
}