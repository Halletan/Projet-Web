namespace SpaceAdventures.MVC.Models;

[Serializable]
public class Flight
{
    public int IdFlight { get; set; }
    public int FlightStatus { get; set; }
    public int IdAircraft { get; set; }
    public double Price { get; set; }
    public int IdItinerary { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
}

[Serializable]
public class Flights
{
    public List<Flight> FlightsList { get; set; }
}