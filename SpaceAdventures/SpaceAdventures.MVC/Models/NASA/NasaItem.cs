namespace SpaceAdventures.MVC.Models.NASA;

public class NasaItem
{
    public string version { get; set; }
    public string href { get; set; }
    public Item[] items { get; set; }

    public LinkCollection[] links { get; set; }

    public Metadata metadata { get; set; }
}