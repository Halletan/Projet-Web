using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Services.Interfaces;
using FluentValidation;

namespace SpaceAdventures.Application.Common.Commands.Itinerary
{
    public class UpdateAirportCommandValidator : AbstractValidator<UpdateAirportCommand>
    {
        public UpdateAirportCommandValidator(IClientService clientService)       
        {
            RuleFor(c => c.ClientInput.FirstName)
                .MaximumLength(50).WithMessage("Max 50 characters")
                .NotNull().WithMessage("Firstname is required");
            RuleFor(c => c.ClientInput.LastName)
                .MaximumLength(50).WithMessage("Max 50 characters")
                .NotNull().WithMessage("Firstname is required");
            RuleFor(c => c.ClientInput.Phone)
                .MaximumLength(50).WithMessage("Max 50 characters")
                .NotNull().WithMessage("Phone is required");
            RuleFor(c => c.ClientInput.Email)
                .MaximumLength(50).WithMessage("Max 50 characters")
                .EmailAddress().WithMessage("Invalid email address")
                .NotNull().WithMessage("Email is required");

            RuleFor(c => c.Id)
                .NotNull().WithMessage("Client Id is required")
                .GreaterThanOrEqualTo(1).WithMessage("Should be greater than or equal to 1")
                .Must((data, id) =>
                {
                    bool exists = clientService.ClientExists(id, data.ClientInput);
                    return !exists;
                }).WithMessage("Client with this email address exists already !");

            RuleFor(c => c.ClientInput.IdMemberShipType)
                .GreaterThanOrEqualTo(1).WithMessage("Should be greater than or equal to 1");
        }
    }
}
