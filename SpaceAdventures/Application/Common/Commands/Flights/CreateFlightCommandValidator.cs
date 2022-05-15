using FluentValidation;
using SpaceAdventures.Application.Common.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Commands.Flights
{
    public class CreateFlightCommandValidator : AbstractValidator<CreateFlightCommand>
    {
        public CreateFlightCommandValidator(IFlightService flightService)
        {
            RuleFor(f => f.flightInput.Price).NotEmpty().WithMessage("Price is mandatory")
                .InclusiveBetween(1,1000000).WithMessage("Price should be between 1 and 1000000");

            RuleFor(f => f.flightInput.DepartureTime).LessThan(f => f.flightInput.ArrivalTime)
                .WithMessage("Departure time should be before arrival time")
                .GreaterThanOrEqualTo(DateTime.Now).WithMessage("Departure time cannot be in the past");
            RuleFor(f => f.flightInput.ArrivalTime).GreaterThan(f => f.flightInput.DepartureTime)
                .WithMessage("Arrival time should be after Departure time")
                .GreaterThanOrEqualTo(DateTime.Now).WithMessage("Arrival time cannot be in the past");


            // control for existing Idaircraft
            // control for existing IdItineray

            // FlightStatus validation ?
        }

    }
}
