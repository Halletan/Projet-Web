using SpaceAdventures.MVC.Models;

namespace SpaceAdventures.MVC.Services.Interfaces
{
    public interface IPlanetService
    {
        Task<Planet> GetPlanetById(int id, string? accessToken);
    }
}
