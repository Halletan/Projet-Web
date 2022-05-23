using FluentValidation;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Commands.Airports;

public class CreateAirportCommandValidator : AbstractValidator<CreateAirportCommand>
{
    public CreateAirportCommandValidator(IAirportService airportService)
    {
        RuleFor(c => c.AirportInput.Name)
            .NotEmpty().WithMessage("Airport's name is mandatory")
            .MaximumLength(50).WithMessage("Airport's name should not exceed 50 characters");

        RuleFor(n => n.AirportInput.IdAirport)
            .Must((data, id) =>
            {
                var exists = airportService.AirportExists(id, data.AirportInput);
                return !exists;
            }).WithMessage("This Airport already exists !");

        RuleFor(c => c.AirportInput.IdPlanet)
            .Must(idplanet =>
            {
                var exists = airportService.PlanetExists(idplanet);
                return !exists;
            }).WithMessage("This planet doesn't exists !");
    }
}