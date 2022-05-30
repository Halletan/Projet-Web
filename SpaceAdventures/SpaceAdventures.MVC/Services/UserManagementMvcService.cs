using Newtonsoft.Json;
using SpaceAdventures.MVC.Models;
using SpaceAdventures.MVC.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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

        public async Task<User> CreateUser( string accessToken, UserInput user)
        {

            /*  _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
             User u = new User
              {
                  Email = user.Email,
                  Username = user.Username,
                  Connection = "Username-Password-Authentication"
              };*/

            //await CreateUserInDb(accessToken,user);


           // Create user in Auth0 (Call API Auth0)
           // Create user in DB (call API)
           // Give userRole set by admin
           // Give customer role in Auth0
           //Else  (modified user that already exist) or send error message ? 
           //Else Access denied



            // Check if UserExists in DB by calling our APIController
            //...



            //if Ok, Create User on Auth0 by calling Auth0 Management API
            string result = await CreateUserOnAuth0(accessToken, user);




            // Else, display message

            // IF OK create user in Auth0, Create User in DB

            // Return user to display

            return await CreateUserInDb(accessToken, user);   

                // IF not, ...

            

        }
        public async Task<string> CreateUserOnAuth0(string token, UserInput user)
        {
            try
            {
                User u = new User
                {
                    Email = user.Email,
                    Username = user.Username,
                    //password = "test",
                    Connection = "Username-Password-Authentication"
                    //app_metadata = new Dictionary<string, string>
                    //    {
                    //        {"companyId", "987"},
                    //        {"fakeId", "447"}
                    //    }
                };

                var postBody = JsonConvert.SerializeObject(u);
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = _httpClient.PostAsync(
                    "https://dev-hlpjlsil.eu.auth0.com/api/v2/users", // a changer, faire passer le configuration[Auth0:domain]
                    new StringContent(postBody, Encoding.UTF8, "application/json")).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;

                return responseString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    

        public async Task<User> CreateUserInDb(string? accessToken, UserInput user)
        {
            var postBody = JsonConvert.SerializeObject(user);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.PostAsync("https://localhost:7195/api/v1.0/Users/CreateUser", new StringContent(postBody, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode) throw new Exception("Cannot retrieve data");

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<User>(content);

        }

        public async Task<bool> UserExistsInDb(string? accessToken, UserInput user)
        {
            throw new NotImplementedException();

            // UserExists to implement in SpaceAdventures API Controllers (already implemented in Services)


            //string email = user.Email;

            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            //var response = await _httpClient.GetAsync("https://localhost:7195/api/v1.0/Users/UserExists/"+email);

            //if (!response.IsSuccessStatusCode) throw new Exception("Cannot retrieve data");

            //var content = await response.Content.ReadAsStringAsync();
            //return JsonConvert.DeserializeObject<bool>(content);
        }

    }
}
