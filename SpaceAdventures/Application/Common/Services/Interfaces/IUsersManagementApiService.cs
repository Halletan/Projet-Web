using SpaceAdventures.Application.Common.Models.UsersAuth0ManagementApi;

namespace SpaceAdventures.Application.Common.Services.Interfaces;

public interface IUsersManagementApiService
{
    Task<Roles> GetUserRoles(string userId, bool includeTotals = false, CancellationToken cancellation = default);
Task<TokenData> GetToken();  // APi Management Auth0

}