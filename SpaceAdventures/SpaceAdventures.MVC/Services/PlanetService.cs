using Newtonsoft.Json;
using SpaceAdventures.MVC.Models;
using SpaceAdventures.MVC.Services.Interfaces;
using System.Net.Http.Headers;

namespace SpaceAdventures.MVC.Services
{
    public class PlanetService : IPlanetService
    {
        private readonly HttpClient _httpClient;
        
        public PlanetService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            
        }

        public async Task<Planet> GetPlanetById(int id, string? accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.GetAsync("https://localhost:7195/api/v1.0/Planets/GetPlanetById/" + id);

            if (!response.IsSuccessStatusCode) throw new Exception("Cannot retrieve data");

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Planet>(content);
        }
    }
}
