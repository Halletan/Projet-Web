using MediatR;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Commands.Membership;

public record DeleteMembershipCommand(int Id) : IRequest;

public class DeleteClientCommandHandler : IRequestHandler<DeleteMembershipCommand>
{
    private readonly IMembershipService _membershipService;

    public DeleteClientCommandHandler(IMembershipService membershipService)
    {
        _membershipService = membershipService;
    }

    public async Task<Unit> Handle(DeleteMembershipCommand request, CancellationToken cancellationToken)
    {
        await _membershipService.DeleteMembership(request.Id, cancellationToken);
        return Unit.Value;
    }
}