using SpaceAdventures.MVC.Models;

namespace SpaceAdventures.MVC.Services.Interfaces;

public interface IFlightService
{
    Task<Flights> GetAllFlights(string? accessToken);
}