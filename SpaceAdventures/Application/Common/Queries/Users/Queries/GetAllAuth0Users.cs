
using MediatR;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Models.UsersAuth0ManagementApi;

namespace SpaceAdventures.Application.Common.Queries.Users.Queries
{
    public record GetAllAuth0Users : IRequest<List<UserAuth0>>;

    public class GetAllAuth0UsersHandler : IRequestHandler<GetAllAuth0Users, List<UserAuth0>>
    {
        private readonly IUsersManagementApiService _usersManagementApiService;

        public GetAllAuth0UsersHandler(IUsersManagementApiService usersManagementApiService)
        {
            _usersManagementApiService = usersManagementApiService;
        }

        public async Task<List<UserAuth0>> Handle(GetAllAuth0Users request, CancellationToken cancellationToken)
        {
            return await _usersManagementApiService.GetAllAuth0Users(cancellationToken);
        }
    }
}
