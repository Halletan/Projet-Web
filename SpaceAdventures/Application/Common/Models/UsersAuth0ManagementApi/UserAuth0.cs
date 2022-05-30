using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using Newtonsoft.Json;
using SpaceAdventures.Domain.Entities;

namespace SpaceAdventures.Application.Common.Models.UsersAuth0ManagementApi
{
    [Serializable]
    public class UserAuth0 : IMapFrom<User>
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("email_verified")]
        public bool VerifiedEmail { get; set; }
        public string app_metadata { get; set; }
        public string given_name { get; set; }
        public string family_name { get; set; }
        public string nickname { get; set; }
        public string picture { get; set; }
        [JsonProperty("user_id")]
        public string IdUserAuth0 { get; set; }
        public string connection { get; set; }
        public string password { get; set; }
        public bool verify_email { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserAuth0>();
        }

    }

    

}
