using System.Net.Http.Headers;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SpaceAdventures.Application.Common.Commands.Users;
using SpaceAdventures.Application.Common.Exceptions;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Models.UsersAuth0ManagementApi;
using SpaceAdventures.Application.Common.Queries.Clients;
using SpaceAdventures.Application.Common.Queries.Users.Queries;
using SpaceAdventures.Application.Common.Services.Interfaces;
using SpaceAdventures.Domain.Entities;


namespace SpaceAdventures.Application.Common.Services;

public class UsersManagementApiService : IUsersManagementApiService
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;
    private readonly ISpaceAdventureDbContext _context;
    #region Constructor

    public UsersManagementApiService(IConfiguration configuration, IHttpClientFactory httpClientFactory, IMapper mapper, ISpaceAdventureDbContext context)
    {
        _configuration = configuration;
        _mapper = mapper;
        _context = context;
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

        if (!httpResponse.IsSuccessStatusCode) throw new NotFoundException("Users", userId);

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
                    {"client_id", _configuration["Auth0ManagementApi:ClientId"]},
                    {"grant_type", _configuration["Auth0ManagementApi:GrantType"]},
                    {"client_secret", _configuration["Auth0ManagementApi:ClientSecret"]},
                    {"audience", _configuration["Auth0ManagementApi:Audience"]}
                }));

        if (!response.IsSuccessStatusCode) throw new NotFoundException("Unable to retrieve access token");

        var content = await response.Content.ReadAsStringAsync();
        var tokenData = JsonConvert.DeserializeObject<TokenData>(content);
        return tokenData;
    }
    #endregion


    public async Task<UserDto> CreateUser(UserInput userInput,CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(userInput);

        try
        {
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<UserDto>(user);
        }
        catch (Exception)
        {
            throw new ValidationException();
        }
    }

    public async Task<bool> UserExists(string email)
    {
        return await _context.Users.AnyAsync(c => c.Email==email);
    }
}