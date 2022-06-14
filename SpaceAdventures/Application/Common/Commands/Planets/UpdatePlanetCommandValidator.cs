using FluentValidation;
using SpaceAdventures.Application.Common.Interfaces;

namespace SpaceAdventures.Application.Common.Commands.Planets;

public class UpdatePlanetCommandValidator : AbstractValidator<UpdatePlanetCommand>
{
    public UpdatePlanetCommandValidator(IPlanetService planetService)
    {
        RuleFor(n => n.planetInput.Name)
            .NotEmpty().WithMessage("Planet's name is mandatory")
            .MaximumLength(50).WithMessage("Planet's name should not exceed 50 characters");

        RuleFor(n => n.planetInput.IdPlanet)
            .Must((data, id) =>
            {
                var exists = planetService.PlanetExists(id, data.planetInput);
                return !exists;
            }).WithMessage("This planet already exists !");
    }
}