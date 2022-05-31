using Newtonsoft.Json;
using SpaceAdventures.MVC.Models;
using SpaceAdventures.MVC.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace SpaceAdventures.MVC.Services
{
    public class UserManagementMvcService : IUserManagementMvcService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserManagementMvcService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
           
        }

        public async Task<Users> GetAllUsers(string? accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.GetAsync("https://localhost:7195/api/v1.0/Users/GetAllUsers");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Cannot retrieve data");

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Users>(content);
        }

        public async Task<User> CreateUser(string? accessToken, UserInput user)
        {
           var postBody = JsonConvert.SerializeObject(user);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
           var response = await _httpClient.PostAsync("https://localhost:7195/api/v1.0/Users/CreateUser", new StringContent(postBody, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode) throw new Exception("Cannot retrieve data");

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<User>(content);

        }

        public async Task<UserRole> GetRoleByIdRole(int id, string? accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.GetAsync("https://localhost:7195/api/v1.0/Users/GetRoleByIdRole/" + id);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Cannot retrieve data");

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserRole>(content);

        }

    }
}
