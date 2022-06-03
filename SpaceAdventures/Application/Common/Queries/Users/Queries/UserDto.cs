using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using SpaceAdventures.Application.Common.Queries.Planets;
using SpaceAdventures.Domain.Entities;

namespace SpaceAdventures.Application.Common.Queries.Users.Queries
{
    public class UserDto : IMapFrom<User>
    {
        public int IdUser { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool VerifiedEmail { get; set; }
        public int IdRole { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDto>()
                .ReverseMap();
        }
    }
}
