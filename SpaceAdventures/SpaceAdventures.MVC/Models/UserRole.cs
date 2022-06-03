using System.ComponentModel;
using Newtonsoft.Json;

namespace SpaceAdventures.MVC.Models;

[Serializable]
public class UserRole
{
    [DisplayName("Role")]
    [JsonProperty("id")]
    public string IdRole { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

[Serializable]
public class Roles
{
    public IList<UserRole> RolesList { get; set; }
}