using FluentValidation;

namespace SpaceAdventures.Application.Common.Queries.Clients.GetClientsWithPagination;

public class GetClientsWithPaginationQueryValidator : AbstractValidator<GetClientsWithPaginationQuery>
{
    public GetClientsWithPaginationQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}