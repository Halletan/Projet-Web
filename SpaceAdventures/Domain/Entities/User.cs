using Domain.Entities;

namespace SpaceAdventures.Domain.Entities;

public class User
{
    public int IdUser { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public bool VerifiedEmail { get; set; }
    public int IdRole { get; set; }
    public string IdUserAuth0 { get; set; }

    public virtual Role IdRoleNavigation { get; set; }
    public virtual ICollection<Client> Clients { get; set; }
}