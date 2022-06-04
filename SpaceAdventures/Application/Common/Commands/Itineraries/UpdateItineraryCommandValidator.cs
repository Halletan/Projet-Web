using FluentValidation;
using SpaceAdventures.Application.Common.Interfaces;

namespace SpaceAdventures.Application.Common.Commands.Itineraries;

public class UpdateItineraryCommandValidator : AbstractValidator<UpdateItineraryCommand>
{
    public UpdateItineraryCommandValidator(IItineraryService itineraryService)
    {
        

        RuleFor(c => c.itineraryInput.IdAirport1)
            .NotNull().WithMessage("The starting Airport is mandatory");

        RuleFor(c => c.itineraryInput.IdAirport2)
            .NotNull().WithMessage("The destination Airport is mandatory");

        RuleFor(c => c.itineraryInput)
            .Must(data =>
            {
                var exists = itineraryService.ItineraryExists(data);
                return !exists;
            }).WithMessage("This itinerary already exist !");
    }
}