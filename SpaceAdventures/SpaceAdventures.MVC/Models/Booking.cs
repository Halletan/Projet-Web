namespace SpaceAdventures.MVC.Models;

[Serializable]
public class Booking
{
    public int IdBooking { get; set; }
    public int IdFlight { get; set; }
    public int IdClient { get; set; }
    public double BookingAmount { get; set; }
}

[Serializable]
public class BookingVm
{
    public string Lastname { get; set; }
    public string FirstName { get; set; }

    public string Email { get; set; }

    public int IdItinerary { get; set; }

    public int IdFlight { get; set; }
}

[Serializable]
public class Bookings
{
    public List<Booking> BookingsList { get; set; }
}