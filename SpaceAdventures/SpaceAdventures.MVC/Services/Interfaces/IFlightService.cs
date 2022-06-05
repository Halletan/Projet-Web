using SpaceAdventures.MVC.Models;

namespace SpaceAdventures.MVC.Services.Interfaces;

public interface IFlightService
{
    Task<Flights> GetAllFlights(string? accessToken);
    Task<Flights> GetFlightsByItinerary(int itineraryId, string? accessToken);
    Task<Flight> GetFlightById(int idFlight, string? accessToken);
}