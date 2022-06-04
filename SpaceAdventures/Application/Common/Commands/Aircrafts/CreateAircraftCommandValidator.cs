using FluentValidation;
using SpaceAdventures.Application.Common.Interfaces;

namespace SpaceAdventures.Application.Common.Commands.Aircrafts;

public class CreateAircraftCommandValidator : AbstractValidator<CreateAircraftCommand>
{
    public CreateAircraftCommandValidator(IAircraftService aircraftService)
    {
        RuleFor(a => a.aircraftInput.Manufacturer)
            .NotNull().WithMessage("Manufacturer is mandatory")
            .MaximumLength(50).WithMessage("Manufacturer Name should not be more than 50 characters")
            .NotEmpty().WithMessage("Manufacturer is mandatory");


        RuleFor(a => a.aircraftInput.Model)
            .NotNull().WithMessage("Model is mandatory")
            .MaximumLength(50).WithMessage("Model Name should not be more than 50 characters")
            .NotEmpty().WithMessage("Model is mandatory");

        RuleFor(a => a.aircraftInput.NumberOfSeats)
            .NotNull().WithMessage("NumberOfSeats is mandatory")
            .InclusiveBetween(1, 500).WithMessage("The number of seats shoud be between 1 and 500");
    }
}