using System.ComponentModel;

namespace SpaceAdventures.MVC.Models;

[Serializable]
public class UserRole
{
    [DisplayName("Role")]
    public string IdRole { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

[Serializable]
public class Roles
{
    public IList<UserRole> rolesList { get; set; }

}