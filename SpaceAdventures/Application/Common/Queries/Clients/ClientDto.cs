using Application.Common.Interfaces;
using AutoMapper;
using SpaceAdventures.Domain.Entities;

namespace SpaceAdventures.Application.Common.Queries.Clients;

public class ClientDto : IMapFrom<Client>
{
    public int IdClient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public int? IdMemberShipType { get; set; }
    public int? IdUser { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Client, ClientDto>().ReverseMap();
    }
}