using FluentValidation;
using SpaceAdventures.Application.Common.Interfaces;

namespace SpaceAdventures.Application.Common.Commands.Clients;

public class UpdateClientCommandValidator : AbstractValidator<UpdateClientCommand>
{
    public UpdateClientCommandValidator(IClientService clientService)
    {
        RuleFor(c => c.ClientInput.FirstName)
            .MaximumLength(50).WithMessage("Max 50 characters")
            .NotNull().WithMessage("Firstname is required");
        RuleFor(c => c.ClientInput.LastName)
            .MaximumLength(50).WithMessage("Max 50 characters")
            .NotNull().WithMessage("Firstname is required");

        RuleFor(c => c.ClientInput.Email)
            .MaximumLength(50).WithMessage("Max 50 characters")
            .EmailAddress().WithMessage("Invalid email address")
            .NotNull().WithMessage("Email is required");

        RuleFor(c => c.ClientInput.IdClient)
            .NotNull().WithMessage("Client Id is required")
            .GreaterThanOrEqualTo(1).WithMessage("Should be greater than or equal to 1")
            .Must((data, id) =>
            {
                var exists = clientService.ClientExists(id, data.ClientInput);
                return !exists;
            }).WithMessage("Client with this email address exists already !");

    }
}