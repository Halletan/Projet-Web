using Application.Common.Services.Interfaces;
using FluentValidation;

namespace SpaceAdventures.Application.Common.Commands.Clients;

public class UpdateClientCommandValidator : AbstractValidator<UpdateClientCommand>
{
    public UpdateClientCommandValidator(IClientService clientService)
    {
        RuleFor(c => c.clientInput.FirstName)
            .MaximumLength(50).WithMessage("Max 50 characters")
            .NotNull().WithMessage("Firstname is required");
        RuleFor(c => c.clientInput.LastName)
            .MaximumLength(50).WithMessage("Max 50 characters")
            .NotNull().WithMessage("Firstname is required");
        RuleFor(c => c.clientInput.Phone)
            .MaximumLength(50).WithMessage("Max 50 characters")
            .NotNull().WithMessage("Phone is required");
        RuleFor(c => c.clientInput.Email)
            .MaximumLength(50).WithMessage("Max 50 characters")
            .EmailAddress().WithMessage("Invalid email address")
            .NotNull().WithMessage("Email is required");

        RuleFor(c => c.Id)
            .NotNull().WithMessage("Client Id is required")
            .GreaterThanOrEqualTo(1).WithMessage("Should be greater than or equal to 1")
            .Must((data, id) =>
            {
                var exists = clientService.ClientExists(id, data.clientInput);
                return !exists;
            }).WithMessage("Client with this email address exists already !");

        RuleFor(c => c.clientInput.IdMemberShipType)
            .GreaterThanOrEqualTo(1).WithMessage("Should be greater than or equal to 1");
    }
}