using FluentValidation;
using SpaceAdventures.Application.Common.Commands.Planets;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Planets;

public class CreatePlanetCommandValidator : AbstractValidator<CreatePlanetCommand>
{
    public CreatePlanetCommandValidator(IPlanetService planetService)
    {
        RuleFor(n => n.planetInput.Name)
            .NotEmpty().WithMessage("Planet's name is mandatory")
            .MaximumLength(50).WithMessage("Planet's name should not exceed 50 characters");

        RuleFor(n => n.planetInput.Name)
            .MustAsync(async (name, cancel) =>
            {
                var exists = await planetService.PlanetExists(name);
                return !exists;
            }).WithMessage("This planet already exists !");
    }
}