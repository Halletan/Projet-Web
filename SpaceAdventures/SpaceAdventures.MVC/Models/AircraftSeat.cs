namespace SpaceAdventures.MVC.Models
{

    [Serializable]
    public class AircraftSeat
    {
        public int IdAircraftSeat { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int IdBooking { get; set; }
        public string PassengerLastName { get; set; }
        public string PassengerFirstName { get; set; }
        public int IdFlight { get; set; }
    }

    [Serializable]
    public class AircraftSeats
    {
        public List<AircraftSeat> AircraftSeatsList { get; set; }
    }

}
