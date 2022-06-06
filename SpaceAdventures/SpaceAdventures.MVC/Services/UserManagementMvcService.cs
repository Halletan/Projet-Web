using Newtonsoft.Json;
using SpaceAdventures.MVC.Models;
using SpaceAdventures.MVC.Services.Interfaces;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;



namespace SpaceAdventures.MVC.Services
{
    public class UserManagementMvcService : IUserManagementMvcService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _accessor;

        public UserManagementMvcService(HttpClient httpClient, IHttpContextAccessor accessor)
        {
            _httpClient = httpClient;
            _accessor = accessor;
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
        public async Task<UserDto> GetUserByEmail(string email, string? accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.GetAsync("https://localhost:7195/api/v1.0/Users/GetUserByEmail/" + email);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Cannot retrieve data");

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserDto>(content);
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
        public async Task<bool> DeleteUser(string? accessToken,int userId)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.DeleteAsync("https://localhost:7195/api/v1.0/Users/DeleteUser/" + userId);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Cannot delete data");
            return true;
        }

        public async Task<User> UpdateUser(string? accessToken,int userId, UserInput userInput)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var postBody = JsonConvert.SerializeObject(userInput);

            var response = await _httpClient.PatchAsync("https://localhost:7195/api/v1.0/Users/UpdateUser"+userId, new StringContent(postBody, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
                throw new Exception("Cannot Update data");

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
        public async Task<Roles> GetAllRole(string? accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.GetAsync("https://localhost:7195/api/v1.0/Users/GetAllRole");
            if (!response.IsSuccessStatusCode)
                throw new Exception("Cannot retrieve data");

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Roles>(content);

            return result;
        }
        public async Task<List<UserRole>> GetUserRole(string id, string? accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            try
            {
                var response = await _httpClient.GetAsync($"https://localhost:7195/api/v1.0/Users/UserRoles/{id}");
                if (!response.IsSuccessStatusCode)
                    throw new Exception("Cannot retrieve data");

                var content = await response.Content.ReadAsStringAsync();
                var userRoles = JsonConvert.DeserializeObject<List<UserRole>>(content);

                return userRoles;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public async Task<string> GetRole(string? accessToken)
        {
            var idUser = _accessor.HttpContext.User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;
            var roles = await GetUserRole(idUser, accessToken);
            return await Task.FromResult(roles[0].Name);
        }
    }   
}
