using SpaceAdventures.MVC.Models;

namespace SpaceAdventures.MVC.Services.Interfaces;

public interface IItineraryService
{
    Task<Itineraries> GetAllItineraries(string? accessToken);

    Task<Itineraries> GetItinerariesByDestinationPlanet(string planetName, string? accessToken);

}