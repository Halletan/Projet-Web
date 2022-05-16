namespace SpaceAdventures.MVC.Models
{

    [Serializable]
    public class Client 
    {
        public int IdClient { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int? IdMemberShipType { get; set; }
    }

    [Serializable]
    public class Clients
    {
        public List<Client> ClientsList { get; set; }
    }

}   
