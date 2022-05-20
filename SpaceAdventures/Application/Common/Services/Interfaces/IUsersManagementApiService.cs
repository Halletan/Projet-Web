using SpaceAdventures.Application.Common.Models;
using SpaceAdventures.Application.Common.Models.UsersAuth0ManagementApi;

namespace SpaceAdventures.Application.Common.Services.Interfaces;

public interface IUsersManagementApiService
{
    Task<List<UserRole>> GetUserRoles(string userId, CancellationToken cancellation = default);
    Task<TokenData> GetToken();  // APi Management Auth0

}   