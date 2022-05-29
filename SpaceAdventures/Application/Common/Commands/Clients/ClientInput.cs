using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace SpaceAdventures.Application.Common.Commands.Clients;

public class ClientInput : IMapFrom<Client>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public int? IdMemberShipType { get; set; } = null;
    public int? IdUser { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ClientInput, Client>();
    }
}