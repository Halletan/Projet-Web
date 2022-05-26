using SpaceAdventures.MVC.Models;

namespace SpaceAdventures.MVC.Services.Interfaces
{
    public interface IUserManagementMvcService
    {
        Task<User> CreateUser(string? accessToken, User user);
        Task<string> CreateUserOnAuth0(string token, User user);
        Task<User> CreateUserInDb(string? accessToken, User user);
        Task<bool> UserExistsInDb(string? accessToken, User user);
    }
}
