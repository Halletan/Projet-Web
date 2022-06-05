using System.Net.Http.Headers;
using Newtonsoft.Json;
using SpaceAdventures.MVC.Models;
using SpaceAdventures.MVC.Services.Interfaces;

namespace SpaceAdventures.MVC.Services;

public class FlightService : IFlightService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public FlightService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Flights> GetAllFlights(string? accessToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var response = await _httpClient.GetAsync("https://localhost:7195/api/v1.0/Flights");

        if (!response.IsSuccessStatusCode) throw new Exception("Cannot retrieve data");

        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Flights>(content);
    }

    public async Task<Flights> GetFlightsByItinerary(int itineraryId, string? accessToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var response = await _httpClient.GetAsync("https://localhost:7195/api/v1.0/Flights/GetFlightsByItinerary/"+itineraryId);

        if (!response.IsSuccessStatusCode) throw new Exception("Cannot retrieve data");

        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Flights>(content);
    }

    public async Task<Flight> GetFlightById(int idFlight, string? accessToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var response = await _httpClient.GetAsync("https://localhost:7195/api/v1.0/Flights/GetFlightById/" + idFlight);

        if (!response.IsSuccessStatusCode) throw new Exception("Cannot retrieve data");

        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Flight>(content);
    }

}