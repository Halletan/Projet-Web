using System.ComponentModel;

namespace SpaceAdventures.MVC.Models;

[Serializable]
public class Client
{
    [DisplayName("ID")]
    public int IdClient { get; set; }

    [DisplayName("Firstname")]
    public string FirstName { get; set; }

    [DisplayName("Lastname")]
    public string LastName { get; set; }

    public string Phone { get; set; }
    public string Email { get; set; }

    [DisplayName("Membership")]
    public int? IdMemberShipType { get; set; }
}

[Serializable]
public class Clients
{
    public List<Client> ClientsList { get; set; }
}