namespace SpaceAdventures.Application.Common.Models.APIConsume;

[Serializable]
public class ISSCLPosition
{
    public string message { get; set; }
    public double timestamp { get; set; }
    public Iss_position iss_position { get; set; }
}