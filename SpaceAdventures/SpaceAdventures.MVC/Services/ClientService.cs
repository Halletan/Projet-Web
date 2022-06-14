using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using Newtonsoft.Json;
using SpaceAdventures.MVC.Models;
using SpaceAdventures.MVC.Services.Interfaces;

namespace SpaceAdventures.MVC.Services;

public class ClientService : IClientService
{
    private readonly HttpClient _httpClient;

    public ClientService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Clients> GetAllClients(string? accessToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var response = await _httpClient.GetAsync("https://localhost:7195/api/v1.0/Clients");

        if (!response.IsSuccessStatusCode) 
            throw new Exception("Cannot retrieve data");

        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Clients>(content);
    }

    public async Task<Client> GeClientByIdUser(int id, string? accessToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var response = await _httpClient.GetAsync("https://localhost:7195/api/v1.0/Clients/GetClientByIdUser/"+id);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Cannot retrieve data");

        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Client>(content);
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

    public async Task<bool> CreateClient(Client client, string? accessToken)
    {
        var postBody = JsonConvert.SerializeObject(client);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var response = await _httpClient.PostAsync("https://localhost:7195/api/v1.0/Clients/Create", new StringContent(postBody, Encoding.UTF8, "application/json"));

        if (!response.IsSuccessStatusCode) throw new Exception("Cannot retrieve data");

        return true;
    }

    public async Task<bool> ClientExist(Client client,string? accessToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var response = await _httpClient.GetAsync("https://localhost:7195/api/v1.0/Clients");

        if (!response.IsSuccessStatusCode) throw new Exception("Cannot retrieve data");

        var content = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<Clients>(content);

        return data.ClientsList.Any(c => c.Email.ToLower().Equals(client.Email.ToLower()));
    }

    public async Task<Client> GetClientByEmail(string Email, string? accessToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var response = await _httpClient.GetAsync("https://localhost:7195/api/v1.0/Clients/GetClientByEmail/" + Email );

        if (!response.IsSuccessStatusCode) return null;

        var content = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<Client>(content);
        return data;
    }

}