namespace SpaceAdventures.MVC.Models.NASA;

public class Item
{
    public string href { get; set; }
    public Data[] data { get; set; }

    public LinkItem[] links { get; set; }
}