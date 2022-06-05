using SpaceAdventures.MVC.Models;

namespace SpaceAdventures.MVC.Services.Interfaces;

public interface IAirportService
{
    Task<Airports> GetAllAirports(string? accessToken);
    Task<Airport> GetAirportById(int id, string? accessToken);
}