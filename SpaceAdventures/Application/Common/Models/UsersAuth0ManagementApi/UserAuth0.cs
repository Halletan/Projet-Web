using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Application.Common.Interfaces;
using AutoMapper;
using Newtonsoft.Json;
using SpaceAdventures.Domain.Entities;

namespace SpaceAdventures.Application.Common.Models.UsersAuth0ManagementApi
{
    public class UserAuth0 : IMapFrom<User>
    {
        [JsonProperty("username")]
        public string? Username { get; set; }

        [JsonProperty("email")]
        public string? Email { get; set; }

        [JsonProperty("email_verified")]
        public bool VerifiedEmail { get; set; }

        [JsonProperty("given_name")]
        [NotMapped]
        public string? GivenName { get; set; }

        [JsonProperty("family_name")]
        [NotMapped]
        public string? FamilyName { get; set; }

        [JsonProperty("user_id")]
        public string? IdUserAuth0 { get; set; }

        [JsonProperty("connection")]
        [NotMapped]
        public string? Connection { get; set; }

        [JsonProperty("verify_email")]
        [NotMapped]
        public bool VerifyEmail { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserAuth0>().ReverseMap();
        }

    }

    

}
