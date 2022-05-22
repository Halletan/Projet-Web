using SpaceAdventures.MVC.Models;

namespace SpaceAdventures.MVC.Services.Interfaces;

public interface IClientService
{
    Task<Clients> GetAllClients(string? accessToken);     
    Task<int> GetClientsCount(string? accessToken);
}