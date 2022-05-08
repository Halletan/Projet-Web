using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;

using Newtonsoft.Json;


namespace SpaceAdventures.Application.Common.Commands.Clients
{
    public class ClientInput : IMapFrom<Client>
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int? IdMemberShipType { get; set; } = null;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ClientInput, Client>();

        }
    }
}
