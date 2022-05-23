namespace SpaceAdventures.MVC.Models;

[Serializable]
public class UserRole
{
    public string id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
}

[Serializable]
public class Roles
{
    public List<UserRole> roles { get; set; }
}