namespace SpaceAdventures.MVC.Models
{
    [Serializable]
    public class Booking
    {
        public int IdBooking { get; set; }
        public int IdFlight { get; set; }
        public int IdClient { get; set; }
        public double BookingAmount { get; set; }
    }

    [Serializable]
    public class Bookings
    {
        public List<Booking> BookingsList { get; set; }
    }
}
