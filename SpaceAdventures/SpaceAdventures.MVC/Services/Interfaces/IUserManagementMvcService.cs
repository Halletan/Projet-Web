using SpaceAdventures.MVC.Models;

namespace SpaceAdventures.MVC.Services.Interfaces
{
    public interface IUserManagementMvcService
    {
        Task<User> CreateUser(string token, UserInput user);
        Task<Users> GetAllUsers(string? accessToken);

        Task<UserRole> GetRoleById(string? accessToken);
    }
}
