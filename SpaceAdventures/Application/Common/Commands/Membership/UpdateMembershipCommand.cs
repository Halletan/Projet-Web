using MediatR;
using SpaceAdventures.Application.Common.Queries.Membership;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Commands.Membership;

public record UpdateMembershipCommand(int Id, MembershipInput membershipInput) : IRequest<MembershipDto>;

public class UpdateClientCommandHandler : IRequestHandler<UpdateMembershipCommand, MembershipDto>
{
    private readonly IMembershipService _membershipService;

    public UpdateClientCommandHandler(IMembershipService membershipService)
    {
        _membershipService = membershipService;
    }

    public async Task<MembershipDto> Handle(UpdateMembershipCommand request, CancellationToken cancellationToken)
    {
        return await _membershipService.UpdateMembership(request.Id, request.membershipInput, cancellationToken);
    }
}