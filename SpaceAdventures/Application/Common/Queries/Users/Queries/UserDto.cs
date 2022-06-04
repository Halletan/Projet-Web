using Application.Common.Interfaces;
using AutoMapper;
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
