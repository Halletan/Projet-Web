using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.EventSource;

namespace SpaceAdventures.Application.Common.Models.APIConsume.NASA
{
    public class Data
    {
        public string center { get; set; }
        public DateTime dateCreated { get; set; }
        string description { get; set; }

        public string[] keywords { get; set; }
        public string mediaType { get; set; }
        public string nasaId { get; set; }
        public string title { get; set; }
    }
}
