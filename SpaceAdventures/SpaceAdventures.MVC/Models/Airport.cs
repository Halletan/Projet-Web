namespace SpaceAdventures.MVC.Models;

[Serializable]
public class Airport
{
    public int IdAirport { get; set; }
    public string Name { get; set; }
    public int IdPlanet { get; set; }
}

[Serializable]
public class Airports
{
    public List<Airport> AirportsList { get; set; }
}