using FluentValidation;
using SpaceAdventures.Application.Common.Interfaces;

namespace SpaceAdventures.Application.Common.Commands.Airports;

public class UpdateAirportCommandValidator : AbstractValidator<UpdateAirportCommand>
{
    public UpdateAirportCommandValidator(IAirportService airportService)
    {
        RuleFor(c => c.airportInput.Name)
            .NotEmpty().WithMessage("Airport's name is mandatory")
            .MaximumLength(50).WithMessage("Airport's name should not exceed 50 characters");

        RuleFor(n => n.airportInput.IdAirport)
            .Must((data, id) =>
            {
                var exists = airportService.AirportExists(id, data.airportInput);
                return !exists;
            }).WithMessage("This Airport already exists !");

        RuleFor(c => c.airportInput.IdPlanet)
            .Must(idplanet =>
            {
                var exists = airportService.PlanetExists(idplanet);
                return !exists;
            }).WithMessage("This planet doesn't exists !");
    }
}