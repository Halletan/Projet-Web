using SpaceAdventures.Application.Common.Commands.Users;
using SpaceAdventures.Application.Common.Models.UsersAuth0ManagementApi;
using SpaceAdventures.Application.Common.Queries.Users.Queries;
using SpaceAdventures.Domain.Entities;

namespace SpaceAdventures.Application.Common.Interfaces;

public interface IUsersManagementApiService
{
    #region User

    Task<UserDto> GetUserById(int userId, CancellationToken cancellation = default);
    Task<UsersVm> GetAllUsers(CancellationToken cancellation = default);
    Task<UserDto> GetUserByEmail(string email, CancellationToken cancellation = default);
    Task<List<UserAuth0>> GetAllAuth0Users(CancellationToken cancellation = default);   
    Task<bool> UserExists(string email);
    Task<UserDto> CreateUser(UserInput userInput, CancellationToken cancellationToken = default);
    Task<User> CreateUserAuth0(UserInput userInput, CancellationToken cancellationToken = default);
    Task<UserDto> CreateUserInDb(User user, CancellationToken cancellationToken = default);
    Task<UserDto> UpdateUser(int id, UserInput userInput, CancellationToken cancellationToken = default);
    Task<UserDto> UpdateUserInDb(User user, CancellationToken cancellationToken = default);
    Task<User> UpdateUserInAuth0(string idAuth0, UserInput userInput, CancellationToken cancellationToken = default);
    //Task<User> UpdateUserInAuth0(User user, CancellationToken cancellationToken = default);
    Task DeleteUser(int id, CancellationToken cancellation = default);
    Task<bool> DeleteUserInAuth0(User user, CancellationToken cancellationToken = default);
    Task<bool> DeleteUserInDb(User user, CancellationToken cancellationToken = default);

    #endregion

    #region Role
    Task<List<UserRole>> GetAllRoles(CancellationToken cancellation = default);
    Task<List<UserRole>> GetUserRoles(string userId, CancellationToken cancellation = default);
    Task<TokenData> GetToken(); // APi Management Auth0
    Task<bool> AssignRole(User user, CancellationToken cancellationToken = default);
    Task AssignRoles(string id, AssignRolesRequest request, CancellationToken cancellationToken = default);
    Task<RolesVm> GetAllRoleInDb(CancellationToken cancellationToken = default);
    Task<Role> GetRoleInDb(User user, CancellationToken cancellationToken = default);
    Task<RoleDto> GetRoleInDbByIdRole(int id, CancellationToken cancellationToken = default);

    #endregion

}