
using MediatR;
using SpaceAdventures.Application.Common.Models.UsersAuth0ManagementApi;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Queries.Users.Queries
{
    public record GetUserRolesQuery(string UserId, bool IncludeTotals) : IRequest<Roles>;

    public class GetUserRolesQueryHandler : IRequestHandler<GetUserRolesQuery, Roles>
    {
        private readonly IUsersManagementApiService _usersManagementApiService;

        public GetUserRolesQueryHandler(IUsersManagementApiService usersManagementApiService)
        {
            _usersManagementApiService = usersManagementApiService;
        }

        public async Task<Roles> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
        {
            return await _usersManagementApiService.GetUserRoles(request.UserId, request.IncludeTotals, cancellationToken);
        }
    }

}   
