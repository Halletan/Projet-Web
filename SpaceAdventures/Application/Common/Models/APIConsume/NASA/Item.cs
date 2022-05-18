using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Models.APIConsume.NASA
{
    public class Item
    {
        public Data[] data { get; set; }
        public string href { get; set; }
        public Link[] links { get; set; }
    }
}
