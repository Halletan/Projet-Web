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
    public record GetRoleByIdRoleQuery(int id) : IRequest<RoleDto>;

    public class GetRoleByIdRoleQueryHandler : IRequestHandler<GetRoleByIdRoleQuery, RoleDto>
    {
        private readonly IUsersManagementApiService _usersManagementApiService;

        public GetRoleByIdRoleQueryHandler(IUsersManagementApiService usersManagementApiService)
        {
            _usersManagementApiService = usersManagementApiService;
        }

        public async Task<RoleDto> Handle(GetRoleByIdRoleQuery request, CancellationToken cancellationToken)
        {
            return await _usersManagementApiService.GetRoleInDbByIdRole(request.id, cancellationToken);
        }

    }
}
