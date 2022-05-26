using SpaceAdventures.MVC.Models;

namespace SpaceAdventures.MVC.Services.Interfaces
{
    public interface IUserManagementMvcService
    {
        Task<User> CreateUser(string token, UserInput user);
        Task<string> CreateUserOnAuth0(string token, UserInput user);
        Task<User> CreateUserInDb(string? accessToken, UserInput user);
        Task<bool> UserExistsInDb(string? accessToken, UserInput user);
    }
}
