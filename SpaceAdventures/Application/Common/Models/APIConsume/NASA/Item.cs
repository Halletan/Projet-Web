namespace SpaceAdventures.Application.Common.Models.APIConsume.NASA;

public class Item
{
    public string href { get; set; }
    public Data[] data { get; set; }

    public LinkItem[] links { get; set; }
}