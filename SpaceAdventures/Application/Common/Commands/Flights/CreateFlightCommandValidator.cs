using FluentValidation;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Commands.Flights;

public class CreateFlightCommandValidator : AbstractValidator<CreateFlightCommand>
{
    public CreateFlightCommandValidator(IFlightService flightService)
    {
        RuleFor(f => f.flightInput.Price).NotEmpty().WithMessage("Price is mandatory")
            .InclusiveBetween(1, 1000000).WithMessage("Price should be between 1 and 1000000");

        RuleFor(f => f.flightInput.DepartureTime).LessThan(f => f.flightInput.ArrivalTime)
            .WithMessage("Departure time should be before arrival time")
            .GreaterThanOrEqualTo(DateTime.Now).WithMessage("Departure time cannot be in the past");
        RuleFor(f => f.flightInput.ArrivalTime).GreaterThan(f => f.flightInput.DepartureTime)
            .WithMessage("Arrival time should be after Departure time")
            .GreaterThanOrEqualTo(DateTime.Now).WithMessage("Arrival time cannot be in the past");

        // FlightStatus validation ?

        // Control existing IdAircraft
        RuleFor(f => f.flightInput.IdAircraft)
            .Must(idaircraft =>
            {
                var exists = flightService.AircraftExists(idaircraft);
                return !exists;
            }).WithMessage("This aircraft doesn't exist !");

        // Control existing IdItinerary
        RuleFor(f => f.flightInput.IdItinerary)
            .Must(iditinerary =>
            {
                var exists = flightService.ItineraryExists(iditinerary);
                return !exists;
            }).WithMessage("This itinerary doesn't exist !");
    }
}