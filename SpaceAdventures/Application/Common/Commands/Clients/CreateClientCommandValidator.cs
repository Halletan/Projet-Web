using FluentValidation;
using SpaceAdventures.Application.Common.Interfaces;

namespace SpaceAdventures.Application.Common.Commands.Clients;

public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
{
    public CreateClientCommandValidator(IClientService clientService)
    {
        RuleFor(c => c.ClientInput.FirstName)
            .MaximumLength(50).WithMessage("Max 50 characters")
            .NotNull().WithMessage("Firstname is required")
            .NotEmpty().WithMessage("Firstname cannot be empty");

        RuleFor(c => c.ClientInput.LastName)
            .MaximumLength(50).WithMessage("Max 50 characters")
            .NotNull().WithMessage("Lastname is required")
            .NotEmpty().WithMessage("Lastname cannot be empty");
        

        RuleFor(c => c.ClientInput.Email)
            .MaximumLength(50).WithMessage("Max 50 characters")
            .EmailAddress().WithMessage("Invalid email address")
            .NotNull().WithMessage("Email is required")
            .MustAsync(async (email, cancel) =>
            {
                var exists = await clientService.ClientExists(email);
                return !exists;
            }).WithMessage("Client with this email address exists already !");

    }
}