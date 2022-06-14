using Application.Common.Interfaces;
using AutoMapper;
using SpaceAdventures.Domain.Entities;

namespace SpaceAdventures.Application.Common.Commands.Clients;

public class ClientInput : IMapFrom<Client>
{
    public int IdClient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public int? IdUser { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ClientInput, Client>();
    }
}