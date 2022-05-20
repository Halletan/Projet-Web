
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SpaceAdventures.Application.Common.Exceptions;
using SpaceAdventures.Application.Common.Models;
using SpaceAdventures.Application.Common.Models.UsersAuth0ManagementApi;
using SpaceAdventures.Application.Common.Queries.Clients.GetClientsWithPagination;
using SpaceAdventures.Application.Common.Services.Interfaces;


namespace SpaceAdventures.Application.Common.Services
{
    public class UsersManagementApiService : IUsersManagementApiService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        #region Constructor
        public UsersManagementApiService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            this._configuration = configuration;
            _httpClient = httpClientFactory.CreateClient("RetryPolicy");
        }
        #endregion

        #region Get User's Roles
        public async Task<List<UserRole>> GetUserRoles(string userId, CancellationToken cancellation)
        {
            var tokenResponse = await GetToken();   
            var tokenAccess = tokenResponse.access_token;

            var url = _configuration["Auth0ManagementApi:Audience"] + "users/" + userId + "/roles";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenAccess);

            var httpResponse = await _httpClient.GetAsync(url, cancellation);

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new NotFoundException("Users", userId);
            }

            var content = await httpResponse.Content.ReadAsStringAsync(cancellation);
            var roles = JsonConvert.DeserializeObject<List<UserRole>>(content);
            return roles;
        }

        #endregion

        #region Collecting Access Token

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

        #endregion

    }
}
