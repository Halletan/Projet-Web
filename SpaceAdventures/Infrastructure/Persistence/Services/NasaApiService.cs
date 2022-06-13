using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Models.APIConsume;

namespace SpaceAdventures.Infrastructure.Persistence.Services;

public class NasaApiService : INasaApiService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;


    public NasaApiService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClient = httpClientFactory.CreateClient("RetryPolicy");
        _configuration = configuration;
    }

    public async Task<NasaCollection> GetAlbum(string search, CancellationToken cancellation = default)
    {
        var response = await _httpClient.GetAsync(_configuration["NASA:URL"] + search);

        if (!response.IsSuccessStatusCode) throw new Exception(response.StatusCode.ToString());

        var content = await response.Content.ReadAsStringAsync();
        var tasks = JsonConvert.DeserializeObject<NasaCollection>(content);
        return tasks;
    }

    public async Task<List<string>> GetNasaVideo(string path, CancellationToken cancellation = default)
    {
        var response = await _httpClient.GetAsync(path);
        if (!response.IsSuccessStatusCode) throw new Exception(response.StatusCode.ToString());

        var content = await response.Content.ReadAsStringAsync();
        var tasks = JsonConvert.DeserializeObject<List<string>>(content);
        var result = tasks.FindAll(c => c.Contains("small.mp4"));
        return result;
    }
}