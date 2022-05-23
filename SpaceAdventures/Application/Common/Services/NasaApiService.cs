using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SpaceAdventures.Application.Common.Models.APIConsume;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Services;

public class NasaApiService : INasaApiService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;


    public NasaApiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
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
}