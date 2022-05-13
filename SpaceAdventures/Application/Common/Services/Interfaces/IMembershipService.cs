using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceAdventures.Application.Common.Commands.Membership;
using SpaceAdventures.Application.Common.Queries.Membership;

namespace SpaceAdventures.Application.Common.Services.Interfaces
{
    public interface IMembershipService 
    {
        Task<MembershipVm> GetAllMemberships(CancellationToken cancellation = default);
        Task<MembershipDto> GetMembershipById(int MembershipId, CancellationToken cancellation = default);
        Task<MembershipDto> CreateMembership(MembershipInput MembershipInput, CancellationToken cancellation = default);
        Task<MembershipDto> UpdateMembership(int MembershipId, MembershipInput MembershipInput, CancellationToken cancellation = default);
        Task DeleteMembership(int MembershipId, CancellationToken cancellation = default);
        Task<bool> MembershipExists(string name);
        bool MembershipExists(int id, MembershipInput MembershipInput);
    }
}
