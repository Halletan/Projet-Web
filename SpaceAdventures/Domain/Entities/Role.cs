namespace SpaceAdventures.Domain.Entities;

public class Role
{
    public int IdRole { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public virtual ICollection<User> Users { get; set; }
}