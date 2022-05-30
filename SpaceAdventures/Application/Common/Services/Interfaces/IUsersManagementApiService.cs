using SpaceAdventures.Application.Common.Commands.Users;
using SpaceAdventures.Application.Common.Models.UsersAuth0ManagementApi;
using SpaceAdventures.Application.Common.Queries.Users.Queries;
using SpaceAdventures.Domain.Entities;

namespace SpaceAdventures.Application.Common.Services.Interfaces;

public interface IUsersManagementApiService
{
    Task<List<UserRole>> GetUserRoles(string userId, CancellationToken cancellation = default);
    Task<TokenData> GetToken(); // APi Management Auth0
    Task<UserDto> CreateUser(UserInput userInput,CancellationToken cancellationToken);
    Task<User> CreateUserAuth0(UserInput userInput,CancellationToken cancellationToken);
    Task<UserDto> CreateUserInDb(User user, CancellationToken cancellationToken);
    Task<bool> UserExists(string email);
}