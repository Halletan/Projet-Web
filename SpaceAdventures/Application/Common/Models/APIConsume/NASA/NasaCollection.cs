using SpaceAdventures.Application.Common.Models.APIConsume.NASA;

namespace SpaceAdventures.Application.Common.Models.APIConsume;

[Serializable]
public class NasaCollection
{
    public NasaItem collection { get; set; }
}