using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Domain.Entities;

namespace SpaceAdventures.Application.Common.Queries.Users.Queries
{
    public record GetAllRoleQuery : IRequest<RolesVm>;

    public class GetAllRoleQueryHandler : IRequestHandler<GetAllRoleQuery, RolesVm>
    {
        private readonly IUsersManagementApiService _usersManagementApiService;

        public GetAllRoleQueryHandler(IUsersManagementApiService usersManagementApiService)
        {
            _usersManagementApiService = usersManagementApiService;
        }

        public async Task<RolesVm> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
        {
            return await _usersManagementApiService.GetAllRoleInDb(cancellationToken);
        }

    }
}
