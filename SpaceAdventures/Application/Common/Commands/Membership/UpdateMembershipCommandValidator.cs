using FluentValidation;
using SpaceAdventures.Application.Common.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Commands.Membership
{
    public class UpdateMembershipCommandValidator : AbstractValidator<UpdateMembershipCommand>
    {
        public UpdateMembershipCommandValidator(IMembershipService membershipService)
        {
            RuleFor(n => n.membershipInput.Name)
                .NotEmpty().WithMessage("Membership's name is mandatory")
                .MaximumLength(50).WithMessage("Membership's name should not exceed 50 characters")
                .MustAsync(async (name, cancel) =>
                {
                    bool exists = await membershipService.MembershipExists(name);
                    return !exists;
                }).WithMessage("This Membership already exists !");

            RuleFor(n => n.membershipInput.DiscountFactor)
                .NotEmpty().WithMessage("Membership's discount factor is mandatory")
                .GreaterThan(0).WithMessage("Membership's discount factor should be greater than 0%")
                .LessThanOrEqualTo(50).WithMessage("Membership's discount factor shouldn't be greater than 50%");

            RuleFor(n => n.Id)
                .Must((data, id) =>
                {
                    bool exists = membershipService.MembershipExists(id, data.membershipInput);
                    return !exists;
                }).WithMessage("This membership already exists !");
        }
    }
}
