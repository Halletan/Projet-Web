using System.Net.Http.Headers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SpaceAdventures.Application.Common.Commands.Users;
using SpaceAdventures.Application.Common.Exceptions;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Models.UsersAuth0ManagementApi;
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

    public UsersManagementApiService(IConfiguration configuration, HttpClient httpClient, IMapper mapper, ISpaceAdventureDbContext context)
    {
        _configuration = configuration;
        _mapper = mapper;
        _context = context;
        _httpClient = httpClient;
    }

    #endregion



    #region Get All Users
    public async Task<UsersVm> GetAllUsers(CancellationToken cancellationToken)
    {
        return new UsersVm
        {
            UsersList = await _context.Users
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .OrderBy(u => u.IdUser)
                .ToListAsync(cancellationToken)
        };
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

    #region CreateUser
    public async Task<UserDto> CreateUser(UserInput userInput,CancellationToken cancellationToken)
    {
        try
        {
            User user = await CreateUserAuth0(userInput, cancellationToken);
            return await CreateUserInDb(user,cancellationToken);
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

    public async Task<User> CreateUserAuth0(UserInput userInput, CancellationToken cancellationToken)
    {
        var token = await GetToken();
        var accessToken = token.access_token;

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await _httpClient.PostAsync(_configuration["Auth0ManagementApi:Audience"] + "users",new FormUrlEncodedContent(
            new Dictionary<string, string>
            {
                {"email", userInput.Email},
                {"email_verified", "false"},
                {"connection", "Username-Password-Authentication"},
                {"verify_email","true"},
                {"given_name", "John"},
                {"family_name", "Doe"},
                {"name", "John Doe"},
                {"nickname", "Johnny"},
                {"password", "Test1234**/"},
                {"username", userInput.Username}
            }), cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new ValidationException();
        }
        
        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        var userAuth = JsonConvert.DeserializeObject<UserAuth0>(content);

        var userDb = _mapper.Map<User>(userAuth);
        userDb.IdRole = userInput.IdRole;
        userDb.Username = userInput.Username;

        return userDb;
    }

    public async Task<UserDto> CreateUserInDb(User user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<UserDto>(user);
    }
    #endregion

    #region Assign Role to a user

    public async Task<UserDto> AssignRole(int IdRole, User user) // ou directement UserId
    {
        // AccessToken AuthManagement API
        var token = await GetToken();
        var accessToken = token.access_token;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);




        // var response = await _httpClient.PostAsync()

        // Endpoint https://auth0.com/docs/api/management/v2#!/Roles/post_role_users

        throw new NotImplementedException();
    }
    #endregion

}