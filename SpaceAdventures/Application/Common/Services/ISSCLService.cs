using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SpaceAdventures.Application.Common.Models.APIConsume;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Services;

public class ISSCLService : IISSCLService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;


    public ISSCLService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<ISSCLPosition> GetPosition(CancellationToken cancel)
    {
        var response = await _httpClient.GetAsync(_configuration["ISSCL:URL"]);

        if (!response.IsSuccessStatusCode) throw new Exception(response.StatusCode.ToString());

        var content = await response.Content.ReadAsStringAsync();
        var tasks = JsonConvert.DeserializeObject<ISSCLPosition>(content);
        return tasks;
    }
}