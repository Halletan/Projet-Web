namespace SpaceAdventures.MVC.Services.Interfaces;

public interface IGlobalService
{
    Task<int> GetAircraftsCount(string? accessToken);
    Task<int> GetBookingsCount(string? accessToken);
    Task<int> GetUsersCount(string? accessToken);
}   