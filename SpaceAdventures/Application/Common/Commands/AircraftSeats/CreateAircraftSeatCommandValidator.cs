using FluentValidation;
using SpaceAdventures.Application.Common.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Commands.AircraftSeats
{
    public class CreateAircraftSeatCommandValidator : AbstractValidator<CreateAircraftSeatCommand>
    {

        public CreateAircraftSeatCommandValidator(IAircraftSeatService aircraftSeatService)
        {
            RuleFor(a => a.aircraftSeatInput.Price)
               .NotNull().WithMessage("Price is mandatory")
               .InclusiveBetween(1,200).WithMessage("Price of a seat should be between 1 and 200");

            RuleFor(a => a.aircraftSeatInput.PassengerFirstName)
                .NotNull().WithMessage("Passenger firstname is mandatory")
                .MaximumLength(50).WithMessage("Passenger first name should be maximum 50 characters");

            RuleFor(a => a.aircraftSeatInput.PassengerLastName)
                .NotNull().WithMessage("Passenger lastname is mandatory")
                .MaximumLength(50).WithMessage("Passenger lastname should be maximum 50 characters");

            // Control on existing IdBooking
            // Control on existing IdFlight
            // Reg ex for the name

        }


    }
}
