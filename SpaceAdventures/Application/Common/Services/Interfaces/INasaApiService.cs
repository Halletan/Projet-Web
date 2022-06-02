using SpaceAdventures.Application.Common.Models.APIConsume;

namespace SpaceAdventures.Application.Common.Services.Interfaces;

public interface INasaApiService
{
    Task<NasaCollection> GetAlbum(string search, CancellationToken cancellation = default);

    Task<List<string>> GetNasaVideo(string path, CancellationToken cancellation = default);
}