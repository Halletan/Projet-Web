using Application.Common.Interfaces;
using AutoMapper;
using SpaceAdventures.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Queries.Users.Queries
{
    public class RoleDto : IMapFrom<Role>
    {
        public int IdRole { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Role, RoleDto>().ReverseMap();
        }
    }
}
