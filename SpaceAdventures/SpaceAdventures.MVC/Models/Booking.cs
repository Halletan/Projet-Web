using System.ComponentModel;
using Newtonsoft.Json;

namespace SpaceAdventures.MVC.Models;

[Serializable]
public class Booking
{
    public int IdBooking { get; set; }
    public int IdFlight { get; set; }
    public int IdClient { get; set; }
    public int NbSeats { get; set; }

    [JsonIgnore]
    public string Lastname { get; set; }
    [JsonIgnore]
    public string FirstName { get; set; }
    [JsonIgnore]
    public string Email { get; set; }

    [DisplayName("Itinerary")]
    public int IdItinerary { get; set; }

    public string? airport1Name { get; set; } 
    public string? airport2Name { get; set; } 
    public string? planet1Name { get; set; }  
    public string? planet2Name { get; set; }  

}

[Serializable]
public class Bookings
{
    public List<Booking> BookingsList { get; set; }
}

