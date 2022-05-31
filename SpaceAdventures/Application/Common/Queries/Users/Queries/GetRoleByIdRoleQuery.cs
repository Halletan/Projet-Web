using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SpaceAdventures.Application.Common.Services.Interfaces;
using SpaceAdventures.Domain.Entities;

namespace SpaceAdventures.Application.Common.Queries.Users.Queries
{
    public record GetRoleByIdRoleQuery(int id) : IRequest<Role>;

    public class GetRoleByIdRoleQueryHandler : IRequestHandler<GetRoleByIdQuery, Role>
    {
        private readonly IUsersManagementApiService _usersManagementApiService;

        public GetRoleByIdRoleQueryHandler(IUsersManagementApiService usersManagementApiService)
        {
            _usersManagementApiService = usersManagementApiService;
        }

        public async Task<Role> Handle(GetRoleByIdRoleQuery request, CancellationToken cancellationToken)
        {
            return await _usersManagementApiService.GetRoleInDbByIDRole(request.id, cancellationToken);
        }

    }
}
