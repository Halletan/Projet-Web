using SpaceAdventures.MVC.Models;

namespace SpaceAdventures.MVC.Services.Interfaces;

public interface IClientService
{
    Task<Clients> GetAllClients(string? accessToken);
    Task<int> GetClientsCount(string? accessToken);
    Task<bool> ClientExist(Client client,string? accessToken);
    Task<bool> CreateClient(Client client, string? accessToken);
    Task<Client> GetClientByEmail(string Email, string? accessToken);
}