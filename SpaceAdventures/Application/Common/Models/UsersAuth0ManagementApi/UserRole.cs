
using Newtonsoft.Json;

namespace SpaceAdventures.Application.Common.Models.UsersAuth0ManagementApi
{
    [Serializable]
    public class UserRole
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}
    