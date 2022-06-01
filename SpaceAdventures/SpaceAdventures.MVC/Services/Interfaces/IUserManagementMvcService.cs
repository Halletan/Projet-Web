using SpaceAdventures.MVC.Models;

namespace SpaceAdventures.MVC.Services.Interfaces
{
    public interface IUserManagementMvcService
    {
        Task<User> CreateUser(string token, UserInput user);
        Task<Users> GetAllUsers(string? accessToken);
        Task<UserDto> GetUserByEmail(string email,string? accessToken);
        Task<bool> DeleteUser(string? accessToken,int userId);
        Task<UserRole> GetRoleByIdRole(int id, string? accessToken);
        Task<Roles> GetAllRole(string? accessToken);
    }
}
