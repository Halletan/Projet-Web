using Application.Common.Interfaces;
using AutoMapper;
using SpaceAdventures.Domain.Entities;

namespace SpaceAdventures.Application.Common.Commands.Users
{
    public class UserInput:IMapFrom<User>
    {
        public string Username { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Email { get; set; }
        public string Connection { get; set; }
        public string Password { get; set; }
        public int IdRole { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserInput, User>();
        }

    }

}
