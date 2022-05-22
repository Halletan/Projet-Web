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
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.GetAsync("https://localhost:7195/api/v1.0/Aircrafts");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve data");
            }

            var content = await response.Content.ReadAsStringAsync();
            var aircrafts = JsonConvert.DeserializeObject<Aircrafts>(content);
            return aircrafts.AircraftsList.Count;
        }

        public async Task<int> GetBookingsCount(string? accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.GetAsync("https://localhost:7195/api/v1.0/Bookings");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve data");
            }

            var content = await response.Content.ReadAsStringAsync();
            var bookings = JsonConvert.DeserializeObject<Bookings>(content);
            return bookings.BookingsList.Count;
        }


        // TODO To Be implemented once the table remains available
        public async Task<int> GetUsersCount(string? accessToken)
        {
            throw new NotImplementedException();
        }
    }
}
