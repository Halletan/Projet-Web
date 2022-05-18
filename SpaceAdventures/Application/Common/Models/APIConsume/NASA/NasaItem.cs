using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Models.APIConsume.NASA
{
    public class NasaItem
    {
        public string href { get; set; }
        public Item item { get; set; }

        public Link[] link { get; set; }

        public string metadata { get; set; }
        public string version { get; set; }
    }
}
