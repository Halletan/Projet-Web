namespace SpaceAdventures.MVC.Models;

[Serializable]
public class Itinerary
{
    public int IdItinerary { get; set; }
    public int IdAirport1 { get; set; }
    public int IdAirport2 { get; set; }

    public string? airport1Name { get; set; }
    public string? airport2Name { get; set; }
    public string? planet1Name { get; set; }
    public string? planet2Name { get; set; }
}

[Serializable]
public class Itineraries
{
    public List<Itinerary> ItinerariesList { get; set; }
}