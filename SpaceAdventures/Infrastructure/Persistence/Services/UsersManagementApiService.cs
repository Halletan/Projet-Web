using System.Net.Http.Headers;
using System.Text;
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
using SpaceAdventures.Domain.Entities;



namespace SpaceAdventures.Infrastructure.Persistence.Services;

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

    public async Task<List<UserAuth0>> GetAllAuth0Users(CancellationToken cancellation = default)
    {
        var token = await GetToken();
        var accessToken = token.access_token;

        const string fieldsToInclude = "?fields=user_id%2Cusername%2Cemail%2Cemail_verified";
        var endPoint = _configuration["Auth0ManagementApi:Audience"] + "users" + fieldsToInclude;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var response = await _httpClient.GetAsync(endPoint, cancellation);

        if (!response.IsSuccessStatusCode)
        {
            throw new NotFoundException("Unable to retrieve data");
        }
        var content = await response.Content.ReadAsStringAsync(cancellation);
        var users = JsonConvert.DeserializeObject<List<UserAuth0>>(content);
        return users;
    }
    #endregion

    #region Get User's Roles => Using Auth0 User's ID by Hammadi

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

    #region AssignRoles => Using Auth0 User's ID ID by Hammadi
    public async Task AssignRoles(string id, AssignRolesRequest request, CancellationToken cancellationToken = default)
    {
        var tokenResponse = await GetToken();
        var tokenAccess = tokenResponse.access_token;

        var url = _configuration["Auth0ManagementApi:Audience"] + "users/auth0|" + id + "/roles";

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenAccess);

        var payload = JsonConvert.SerializeObject(request);

        var response = await _httpClient.PostAsync(url, new StringContent(payload, Encoding.UTF8, "application/json"), cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new ValidationException();
        }
    }
    #endregion

    #region APi Managemet Access Token by Hammadi

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

    #region Get All Auth0 Roles
    public async Task<List<UserRole>> GetAllRoles(CancellationToken cancellationToken)
    {
        var tokenResponse = await GetToken();
        var tokenAccess = tokenResponse.access_token;
        var url = _configuration["Auth0ManagementApi:Audience"] + "roles";

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenAccess);
        var httpResponse = await _httpClient.GetAsync(url, cancellationToken);
        if (!httpResponse.IsSuccessStatusCode) throw new NotFoundException();

        var content = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
        var roles = JsonConvert.DeserializeObject<List<UserRole>>(content);
        return roles;

    }
    #endregion

    #region CreateUser
    public async Task<UserDto> CreateUser(UserInput userInput,CancellationToken cancellationToken)
    {
        try
        {
            User user = await CreateUserAuth0(userInput, cancellationToken);
            bool ok = await AssignRole(user, cancellationToken);
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

        string passwordToPost;
        if(userInput.Password == "Default")
        {
            passwordToPost = _configuration["UserPassword:Default"];
        }
        else
        {
            passwordToPost = userInput.Password;
        }

        var response = await _httpClient.PostAsync(_configuration["Auth0ManagementApi:Audience"] + "users",new FormUrlEncodedContent(
            new Dictionary<string, string>
            {
                {"email", userInput.Email},
                {"email_verified", "false"},
                {"connection", _configuration["UserPassword:Connection"]},
                {"verify_email","true"},
                {"given_name", userInput.Firstname},
                {"family_name", userInput.Lastname},
                {"name", userInput.Firstname + " " + userInput.Lastname},
                {"nickname", userInput.Username},
                {"password", passwordToPost},
                
                //{"username", userInput.Username},                    // Antoine et Corentin, laissé commenté; Hammadi, to uncomment
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

    #region UpdateUser
    public async Task<UserDto> UpdateUser(int userId, UserInput userInput, CancellationToken cancellationToken = default)
    {
        try
        {
            //User user = await _context.Users.FindAsync(userId); // .AsNoTracking()

            User user = await _context.Users.Where(u => u.IdUser == userId).AsNoTracking().FirstOrDefaultAsync();

            if (user.IdRole != userInput.IdRole)
            {
                bool unassignRoleOk = await UnAssignRole(user, cancellationToken);

                user.IdRole = userInput.IdRole;
                bool assignRoleOk = await AssignRole(user, cancellationToken);
            }

            if(/*user.Username != userInput.Username ||*/ user.Email != userInput.Email)
            {              
                user = await UpdateUserInAuth0(user.IdUserAuth0, userInput, cancellationToken);
                user.IdUser = userId;
            }
            user.Username = userInput.Username;

            return await UpdateUserInDb(user, cancellationToken);

        }
        catch (Exception) {
            throw new ValidationException();
        }
    }

    public async Task<UserDto> UpdateUserInDb(User user, CancellationToken cancellationToken = default)
    {
        try
        {
             _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<UserDto>(user);
        }
        catch (Exception)
        {
            throw new ValidationException();
        }
    }

    public async Task<User> UpdateUserInAuth0(string idAuth0, UserInput userInput, CancellationToken cancellationToken = default)
    {
        try { 
         // Get AccessToken
        var token = await GetToken();
        var accessToken = token.access_token;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            //var payload = JsonConvert.SerializeObject(user); //new StringContent(payload, Encoding.UTF8, "application/json"));

            var response = await _httpClient.PatchAsync(_configuration["Auth0ManagementApi:Audience"] + "users/" + idAuth0, new FormUrlEncodedContent(
           new Dictionary<string, string>
           {
                {"email", userInput.Email}
           }), cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new ValidationException();
        }

        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        var userAuth = JsonConvert.DeserializeObject<UserAuth0>(content);

            //_mapper.Map<User>(userAuth);
            var userDb = new User
            {                              
                Username = userInput.Username,  // from userInput bc not changed in Auth0
                IdRole = userInput.IdRole,      // from userInput bc not present in Auth0

                Email = userAuth.Email,
                VerifiedEmail = userAuth.VerifiedEmail,
                IdUserAuth0 = userAuth.IdUserAuth0
            };

        return userDb;

        }
        catch(Exception)
        {
            throw new ValidationException();
        }
    }

    #endregion

    #region Assign Role to a user
    public async Task<bool> AssignRole(User user,CancellationToken cancellation)
    {
        var token = await GetToken();
        var accessToken = token.access_token;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        List<UserRole> lst = await GetAllRoles(cancellation);
        Role role = await GetRoleInDb(user, cancellation);

        var result = lst.Find(c => c.name == role.Name);

        string[] tab = {result.id};
        AssignRolesRequest rolesRequest = new AssignRolesRequest(tab);
    

        var json = JsonConvert.SerializeObject(rolesRequest);

        var response = await _httpClient.PostAsync(_configuration["Auth0ManagementApi:Audience"] + "users/" + user.IdUserAuth0 + "/roles", new StringContent(json, Encoding.UTF8, "application/json"), cancellation);

        if (!response.IsSuccessStatusCode)
        {
            throw new ValidationException();
        }

        return true;
    }
    #endregion

    #region Remove a role from a user

    public async Task<bool> UnAssignRole(User user, CancellationToken cancellation)
    {
        var token = await GetToken();
        var accessToken = token.access_token;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        List<UserRole> lst = await GetAllRoles(cancellation);
        Role role = await GetRoleInDb(user, cancellation);

        var result = lst.Find(c => c.name == role.Name);

        string[] tab = { result.id };
        AssignRolesRequest rolesRequest = new AssignRolesRequest(tab);



        var json = JsonConvert.SerializeObject(rolesRequest);

        // DeleteAsync does not take a parameter in Body => Bypass this problem with this solution
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Delete,
            RequestUri = new Uri(_configuration["Auth0ManagementApi:Audience"] + "users/" + user.IdUserAuth0 + "/roles"),
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };
        var response = await _httpClient.SendAsync(request);

        //var response = await _httpClient.DeleteAsync(_configuration["Auth0ManagementApi:Audience"] + "users/" + user.IdUserAuth0 + "/roles", cancellation
        ///* new StringContent(json, Encoding.UTF8, "application/json")*/);

        if (!response.IsSuccessStatusCode)
        {
            throw new ValidationException();
        }

        return true;
    }

    #endregion

    #region Get Role By IdRole
    public async Task<RoleDto> GetRoleInDbByIdRole(int id, CancellationToken cancellationToken)
    {
        var role = await _context.Roles.FindAsync(id);
        if (role == null) throw new NotFoundException(nameof(Role), id);
        return _mapper.Map<RoleDto>(role);
    }
    #endregion
    
    #region GetRole from DB
    public async Task<Role> GetRoleInDb(User user, CancellationToken cancellation)
    {
        try
        {
            var userRole = _context.Roles.SingleOrDefault(c => c.IdRole == user.IdRole);
            return userRole;
        }
        catch (Exception)
        {
            throw new NotFoundException(nameof(Role), user.IdRole);
        }
    }

    public async Task<RolesVm> GetAllRoleInDb(CancellationToken cancellation)
    {
        return new RolesVm
        {
            rolesList = await _context.Roles
                .ProjectTo<RoleDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellation)
        };
    }

    #endregion


    public async Task<UserDto> GetUserByEmail(string email, CancellationToken cancellationToken = default)
    {
        return _context.Users
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .SingleOrDefault(n => n.Email == email);

    }


    #region Delete User
    public async Task DeleteUser(int id, CancellationToken cancellationToken = default)
    {
        User user = _context.Users.SingleOrDefault(u => u.IdUser == id);
        await DeleteUserInAuth0(user);
        await DeleteUserInDb(user);
    }

    public async Task<bool> DeleteUserInDb(User user, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.Users.Remove(user);
           await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
        catch (Exception ex) {
            throw ex;
        }
    }

    public async Task<bool> DeleteUserInAuth0(User user, CancellationToken cancellationToken = default)
    {
        // Get AccessToken
        var token = await GetToken();
        var accessToken = token.access_token;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
    
        var response = await _httpClient.DeleteAsync(_configuration["Auth0ManagementApi:Audience"] + "users/" + user.IdUserAuth0);

        if (!response.IsSuccessStatusCode)
        {
            throw new ValidationException();
        }
        return true;
    }
    #endregion
}