using MediatR;
using SpaceAdventures.Application.Common.Interfaces;

namespace SpaceAdventures.Application.Common.Queries.Membership;

public record GetMembershipByIdQuery(int Id) : IRequest<MembershipDto>;

public class GetMembershipByIdQueryHandler : IRequestHandler<GetMembershipByIdQuery, MembershipDto>
{
    private readonly IMembershipService _membershipService;

    public GetMembershipByIdQueryHandler(IMembershipService membershipService)
    {
        _membershipService = membershipService;
    }

    public async Task<MembershipDto> Handle(GetMembershipByIdQuery request, CancellationToken cancellationToken)
    {
        return await _membershipService.GetMembershipById(request.Id, cancellationToken);
    }
}