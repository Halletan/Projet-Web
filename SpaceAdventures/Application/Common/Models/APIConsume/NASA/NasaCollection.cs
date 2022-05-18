using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceAdventures.Application.Common.Models.APIConsume.NASA;

namespace SpaceAdventures.Application.Common.Models.APIConsume
{
    [Serializable]
    public class NasaCollection
    {
        public List<NasaItem> NasaItems { get; set; }
    }
}
