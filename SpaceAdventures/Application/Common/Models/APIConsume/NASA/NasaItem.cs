﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Models.APIConsume.NASA
{
    public class NasaItem
    {
        public string version { get; set; }
        public string href { get; set; }
        public Item[] items { get; set; }

        public LinkCollection[] links { get; set; }

        public Metadata metadata { get; set; }
        
    }
}
