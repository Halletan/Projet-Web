namespace SpaceAdventures.MVC.Models.NASA;

public class Data
{
    public string[] album;
    public string center { get; set; }
    public string title { get; set; }
    public string nasa_id { get; set; }
    public string media_Type { get; set; }
    public DateTime date_created { get; set; }
    private string description { get; set; }
}