
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SpaceAdventures.Application.Common.Exceptions;
using SpaceAdventures.Application.Common.Models.UsersAuth0ManagementApi;
using SpaceAdventures.Application.Common.Services.Interfaces;


namespace SpaceAdventures.Application.Common.Services
{
    public class UsersManagementApiService : IUsersManagementApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public UsersManagementApiService(HttpClient httpClient, IConfiguration configuration)
        {
            this._httpClient = httpClient;
            this._configuration = configuration;
        }


        public async Task<Roles> GetUserRoles(string userId, bool includeTotals,
            CancellationToken cancellation) 
        {
            var tokenResponse = await GetToken();
            var tokenAccess = tokenResponse.access_token;

            var requestUrl =
                $"https://dev-etahkomt.us.auth0.com/api/v2/users/{userId}/roles?include_totals=true";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenAccess);

            var httpResponse = await _httpClient.GetAsync(requestUrl, cancellation);

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new NotFoundException("Users", userId);
            }

            var content = await httpResponse.Content.ReadAsStringAsync(cancellation);
            var roles = JsonConvert.DeserializeObject<Roles>(content);
            return roles;
        }

        public async Task<TokenData> GetToken()
        {
            var response =
            await _httpClient.PostAsync(_configuration["Auth0ManagementApi:Path"], new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    { "client_id", _configuration["Auth0ManagementApi:ClientId"] },
                    { "grant_type", _configuration["Auth0ManagementApi:GrantType"] },
                    { "client_secret", _configuration["Auth0ManagementApi:ClientSecret"] },
                    { "audience", _configuration["Auth0ManagementApi:Audience"] },

                }));

            if (!response.IsSuccessStatusCode)
            {
                throw new NotFoundException("Unable to retrieve access token");
            }

            var content = await response.Content.ReadAsStringAsync();
            var tokenData = JsonConvert.DeserializeObject<TokenData>(content);
            return tokenData;
        }
    }
}
