namespace SpaceAdventures.MVC.Models;

[Serializable]
public class Aircraft
{
    public int IdAircraft { get; set; }
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public int NumberOfSeats { get; set; }
}

[Serializable]
public class Aircrafts
{
    public List<Aircraft> AircraftsList { get; set; }
}