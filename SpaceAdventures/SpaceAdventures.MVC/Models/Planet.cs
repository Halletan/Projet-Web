namespace SpaceAdventures.MVC.Models
{
    [Serializable]
    public class Planet
    {
        public int IdPlanet { get; set; }
        public string Name { get; set; }
    }

    [Serializable]
    public class Planets
    {
        public List<Planet> PlanetsList { get; set; }
    }
}
