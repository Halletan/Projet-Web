using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpaceAdventures.Application.Common.Models.UsersAuth0ManagementApi
{
    /// <summary>
    /// Contains details of roles that should be assigned to a user.
    /// </summary>
    public class AssignRolesRequest
    {
        /// <summary>
        /// Role IDs to assign to the user.
        /// </summary>
        [JsonProperty("roles")]
        public string[] Roles { get; set; }
    }
}
