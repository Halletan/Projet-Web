using FluentValidation;
using SpaceAdventures.Application.Common.Interfaces;

namespace SpaceAdventures.Application.Common.Commands.AircraftSeats;

public class UpdateAircraftSeatCommandValidator : AbstractValidator<UpdateAircraftSeatCommand>
{
    public UpdateAircraftSeatCommandValidator(IAircraftSeatService aircraftSeatService)
    {
        RuleFor(a => a.aircraftSeatInput.Price)
            .NotNull().WithMessage("Price is mandatory")
            .InclusiveBetween(1, 200).WithMessage("Price of a seat should be between 1 and 200");

        RuleFor(a => a.aircraftSeatInput.PassengerFirstName)
            .NotNull().WithMessage("Passenger firstname is mandatory")
            .MaximumLength(50).WithMessage("Passenger first name should be maximum 50 characters");

        RuleFor(a => a.aircraftSeatInput.PassengerLastName)
            .NotNull().WithMessage("Passenger lastname is mandatory")
            .MaximumLength(50).WithMessage("Passenger lastname should be maximum 50 characters");

        // Control on existing IdBooking
        RuleFor(a => a.aircraftSeatInput.IdBooking)
            .Must(idbooking =>
            {
                var exists = aircraftSeatService.BookingExists(idbooking);
                return !exists;
            }).WithMessage("This booking doesn't exist !");

        // Control on existing IdFlight
        RuleFor(a => a.aircraftSeatInput.IdFlight)
            .Must(idflight =>
            {
                var exists = aircraftSeatService.FlightExists(idflight);
                return !exists;
            }).WithMessage("This flight doesn't exist !");


        // Reg ex for the name
    }
}