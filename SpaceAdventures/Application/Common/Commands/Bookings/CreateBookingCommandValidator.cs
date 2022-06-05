using FluentValidation;
using SpaceAdventures.Application.Common.Interfaces;

namespace SpaceAdventures.Application.Common.Commands.Bookings;

public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
{
    public CreateBookingCommandValidator(IBookingService bookingService)
    {
        

        // Control existing IdFlight
        RuleFor(b => b.bookingInput.IdFlight)
            .Must(idflight =>
            {
                var exists = bookingService.FlightExists(idflight);
                return exists;
            }).WithMessage("This flight doesn't exist !");

        // Control existing IdClient
        RuleFor(b => b.bookingInput.IdClient)
            .Must(idclient =>
            {
                var exists = bookingService.ClientExists(idclient);
                return exists;
            }).WithMessage("This client doesn't exist !");

        // TO DO . Rule for NbSeats



    }
}