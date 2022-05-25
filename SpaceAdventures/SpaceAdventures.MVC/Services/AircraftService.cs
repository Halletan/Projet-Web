using System.Net.Http.Headers;
using Newtonsoft.Json;
using SpaceAdventures.MVC.Models;
using SpaceAdventures.MVC.Services.Interfaces;

namespace SpaceAdventures.MVC.Services;

public class AircraftService : IAircraftService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AircraftService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Aircrafts> GetAllAircrafts(string? accessToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var response = await _httpClient.GetAsync("https://localhost:7195/api/v1.0/Aircrafts");

        if (!response.IsSuccessStatusCode) throw new Exception("Cannot retrieve data");

        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Aircrafts>(content);
    }
}