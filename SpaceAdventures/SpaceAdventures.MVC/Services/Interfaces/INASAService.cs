using SpaceAdventures.MVC.Models.NASA;

namespace SpaceAdventures.MVC.Services.Interfaces
{
    public interface INASAService
    {
        Task<NasaCollection> GetNasaData(string planetName, string? accessToken);
        Task<string> GetNasaVideo(NasaCollection nasaCollection, string? accessToken);
    }
}
