using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using SpaceAdventures.Application.Common.Queries.Users.Queries;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Commands.Users
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator(IUsersManagementApiService UserManagementApiService)
        {
            RuleFor(n => n.userInput.Email)
                .NotEmpty().WithMessage("Email is mandatory")
                .EmailAddress().WithMessage("Invalid email address")
                .MustAsync(async (email, cancel) =>
                {
                    var exists = await UserManagementApiService.UserExists(email);
                    return !exists;
                }).WithMessage("User with this email address exists already !");

       
        }
    }
}
