using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Domain.Entities
{
    public partial class Role
    {
        public int IdRole { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
