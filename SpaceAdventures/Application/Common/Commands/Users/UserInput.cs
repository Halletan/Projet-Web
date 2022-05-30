using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using SpaceAdventures.Application.Common.Commands.Planets;
using SpaceAdventures.Domain.Entities;

namespace SpaceAdventures.Application.Common.Commands.Users
{
    public class UserInput:IMapFrom<User>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public int IdRole { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserInput, User>();
        }

    }

}
