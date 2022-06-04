using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Models.UsersAuth0ManagementApi;

namespace SpaceAdventures.Application.Common.Commands.Users
{
    public record AssignRolesCommand(string Id, AssignRolesRequest Roles) : IRequest;   


    public class AssignRolesCommandHandler : IRequestHandler<AssignRolesCommand>
    {
        private readonly IUsersManagementApiService _usersManagementApiService;

        public AssignRolesCommandHandler(IUsersManagementApiService usersManagementApiService)
        {
            _usersManagementApiService = usersManagementApiService;
        }

        public async Task<Unit> Handle(AssignRolesCommand request, CancellationToken cancellationToken)
        {
            await _usersManagementApiService.AssignRoles(request.Id, request.Roles, cancellationToken);
            return Unit.Value;
        }
    }
}
        
