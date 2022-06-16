using SpaceAdventures.MVC.Models;

namespace SpaceAdventures.MVC.Services.Interfaces
{
    public interface IUserManagementMvcService
    {
        Task<User> CreateUser(string token, User user);
        Task<User> CreateUserSignUp(string? accessToken, User user);
        Task<User> GetUserById(int id, string? accessToken);
        Task<Users> GetAllUsers(string? accessToken);
        Task<Users> GetAllUsersForSignUp();
        Task<User> GetUserByEmail(string email,string? accessToken);
        Task<bool> DeleteUser(string? accessToken,int userId);
        Task<User> UpdateUser(string? accessToken, User user);
        
        Task<UserRole> GetRoleByIdRole(int id, string? accessToken);
        Task<Roles> GetAllRole(string? accessToken);
        Task<List<UserRole>> GetUserRole(string id, string? accessToken);
        Task<string> GetRole(string? accessToken);
    }
}
    