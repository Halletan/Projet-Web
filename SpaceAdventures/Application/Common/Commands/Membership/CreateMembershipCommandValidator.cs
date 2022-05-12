﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using SpaceAdventures.Application.Common.Commands.Membership;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Membership
{
    public class CreateMembershipCommandValidator : AbstractValidator<CreateMembershipCommand>
    {
        public CreateMembershipCommandValidator (IMembershipService membershipService)
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
        }

    }
}
