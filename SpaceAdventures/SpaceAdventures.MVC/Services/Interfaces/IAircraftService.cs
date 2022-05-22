using Newtonsoft.Json;
using SpaceAdventures.MVC.Models;

namespace SpaceAdventures.MVC.Services.Interfaces
{
    public interface IAircraftService
    {
        Task<Aircrafts> GetAllAircrafts(string? accessToken);

    }
}
