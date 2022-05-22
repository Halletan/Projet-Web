using SpaceAdventures.MVC.Models;
using SpaceAdventures.MVC.Services.Interfaces;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace SpaceAdventures.MVC.Services
{
    public class BookingService : IBookingService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BookingService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Bookings> GetAllBookings(string? accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.GetAsync("https://localhost:7195/api/v1.0/Bookings");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve data");
            }

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Bookings>(content);
        }

    }
}
