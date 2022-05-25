using System.Net.Http.Headers;
using Newtonsoft.Json;
using SpaceAdventures.MVC.Models;
using SpaceAdventures.MVC.Services.Interfaces;

namespace SpaceAdventures.MVC.Services;

public class ClientService : IClientService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ClientService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
    }


    public async Task<Clients> GetAllClients(string? accessToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var response = await _httpClient.GetAsync("https://localhost:7195/api/v1.0/Clients");

        if (!response.IsSuccessStatusCode) throw new Exception("Cannot retrieve data");

        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Clients>(content);
    }

    public async Task<int> GetClientsCount(string? accessToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var response = await _httpClient.GetAsync("https://localhost:7195/api/v1.0/Clients");

        if (!response.IsSuccessStatusCode) throw new Exception("Cannot retrieve data");

        var content = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<Clients>(content);
        return data.ClientsList.Count;
    }
}