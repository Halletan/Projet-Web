using FluentValidation;
using SpaceAdventures.Application.Common.Interfaces;

namespace SpaceAdventures.Application.Common.Commands.Membership;

public class UpdateMembershipCommandValidator : AbstractValidator<UpdateMembershipCommand>
{
    public UpdateMembershipCommandValidator(IMembershipService membershipService)
    {
        RuleFor(n => n.membershipInput.Name)
            .NotEmpty().WithMessage("Membership's name is mandatory")
            .MaximumLength(50).WithMessage("Membership's name should not exceed 50 characters");

        RuleFor(n => n.membershipInput.DiscountFactor)
            .NotEmpty().WithMessage("Membership's discount factor is mandatory")
            .GreaterThan(0).WithMessage("Membership's discount factor should be greater than 0%")
            .LessThanOrEqualTo(50).WithMessage("Membership's discount factor shouldn't be greater than 50%");

        RuleFor(n => n.Id)
            .Must((data, id) =>
            {
                var exists = membershipService.MembershipExists(id, data.membershipInput);
                return !exists;
            }).WithMessage("This membership already exists !");
    }
}