using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Queries.Membership;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Commands.Membership
{
    public record CreateMembershipCommand(MembershipInput membershipInput) : IRequest<MembershipDto>;

    public class CreateMembershipCommandHandler : IRequestHandler<CreateMembershipCommand, MembershipDto>
    {
        private readonly IMembershipService _membershipService;


        public CreateMembershipCommandHandler(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        public async Task<MembershipDto> Handle(CreateMembershipCommand command, CancellationToken cancellationToken)
        {
            return await _membershipService.CreateMembership(command.membershipInput,cancellationToken);
        }
    }
}
