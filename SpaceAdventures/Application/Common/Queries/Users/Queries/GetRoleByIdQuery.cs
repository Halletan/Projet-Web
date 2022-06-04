
using MediatR;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Domain.Entities;

namespace SpaceAdventures.Application.Common.Queries.Users.Queries
{
    public record GetRoleByIdQuery(User User) : IRequest<Role>;

    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, Role>
    {
        private readonly IUsersManagementApiService _usersManagementApiService;

        public GetRoleByIdQueryHandler(IUsersManagementApiService usersManagementApiService)
        {
            _usersManagementApiService = usersManagementApiService;
        }

        public async Task<Role> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            return await _usersManagementApiService.GetRoleInDb(request.User, cancellationToken);
        }

    }
}
