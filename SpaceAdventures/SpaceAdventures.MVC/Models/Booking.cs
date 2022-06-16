using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SpaceAdventures.MVC.Models;

[Serializable]
public class Booking
{
    public int IdBooking { get; set; }

    [Required]
    [Range(1,int.MaxValue)]
    public int IdFlight { get; set; }

    public int IdClient { get; set; }

    [Required]
    [Range(1,int.MaxValue)]
    public int NbSeats { get; set; }

    [Required]
    [MaxLength(20)]
    [MinLength(5)]
    [JsonIgnore]
    public string Lastname { get; set; }

    [Required]
    [MaxLength(20)]
    [MinLength(5)]
    [JsonIgnore]
    public string FirstName { get; set; }

    [Required]
    [EmailAddress]
    [JsonIgnore]
    public string Email { get; set; }

    [Required]
    [DisplayName("Itinerary")]
    public int IdItinerary { get; set; }

    public string? clientFirstName { get; set; }
    public string? clientLastName { get; set; }
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

