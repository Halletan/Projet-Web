using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Models.UsersAuth0ManagementApi
{
    [Serializable]
    public class UserRole
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }

    [Serializable]
    public class Roles
    {
        public List<UserRole> roles { get; set; }
    }
}
