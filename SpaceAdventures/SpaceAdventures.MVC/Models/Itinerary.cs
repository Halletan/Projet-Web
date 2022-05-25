namespace SpaceAdventures.MVC.Models;

[Serializable]
public class Itinerary
{
    public int IdItinerary { get; set; }
    public int IdAirport1 { get; set; }
    public int IdAirport2 { get; set; }
    public double Rate { get; set; }
}

[Serializable]
public class Itineraries
{
    public List<Itinerary> ItinerariesList { get; set; }
}