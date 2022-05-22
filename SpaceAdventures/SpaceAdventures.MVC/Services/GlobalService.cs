using System.Net.Http.Headers;
using Newtonsoft.Json;
using SpaceAdventures.MVC.Models;
using SpaceAdventures.MVC.Services.Interfaces;

namespace SpaceAdventures.MVC.Services
{

    public class GlobalService : IGlobalService
    {

        private readonly HttpClient _httpClient;

        public GlobalService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetAircraftsCount(string? accessToken)
        {
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            //var response = await _httpClient.GetAsync("https://localhost:7195/api/v1.0/");

            //if (!response.IsSuccessStatusCode)
            //{
            //    throw new Exception("Cannot retrieve data");
            //}

            //var content = await response.Content.ReadAsStringAsync();
            //var data = JsonConvert.DeserializeObject<>(content);
            throw new NotImplementedException();

        }

        public async Task<int> GetBookingsCount(string? accessToken)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetUsersCount(string? accessToken)
        {
            throw new NotImplementedException();
        }
    }
}
