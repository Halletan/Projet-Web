using FluentValidation;
using SpaceAdventures.Application.Common.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Commands.Bookings
{
    public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingCommandValidator(IBookingService bookingService)
        {

            RuleFor(b => b.bookingInput.BookingAmount).NotEmpty().WithMessage("Booking amount is mandatory")
                .InclusiveBetween(1,1000000).WithMessage("Booking amount should be between 1 and 1000000");

            // Control existing IdFlight
            RuleFor(b => b.bookingInput.IdFlight)
                .Must((idflight) =>
                {
                    bool exists = bookingService.FlightExists(idflight);
                    return !exists;
                }).WithMessage("This flight doesn't exist !");

            // Control existing IdClient
            RuleFor(b => b.bookingInput.IdClient)
                .Must((idclient) =>
                {
                    bool exists = bookingService.ClientExists(idclient);
                    return !exists;
                }).WithMessage("This client doesn't exist !");

        }

    }
}
