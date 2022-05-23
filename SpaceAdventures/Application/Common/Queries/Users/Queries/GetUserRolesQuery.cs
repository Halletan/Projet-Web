using MediatR;
using SpaceAdventures.Application.Common.Models.UsersAuth0ManagementApi;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Queries.Users.Queries;

public record GetUserRolesQuery(string UserId) : IRequest<List<UserRole>>;

public class GetUserRolesQueryHandler : IRequestHandler<GetUserRolesQuery, List<UserRole>>
{
    private readonly IUsersManagementApiService _usersManagementApiService;

    public GetUserRolesQueryHandler(IUsersManagementApiService usersManagementApiService)
    {
        _usersManagementApiService = usersManagementApiService;
    }

    public async Task<List<UserRole>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
    {
        return await _usersManagementApiService.GetUserRoles(request.UserId, cancellationToken);
    }
}