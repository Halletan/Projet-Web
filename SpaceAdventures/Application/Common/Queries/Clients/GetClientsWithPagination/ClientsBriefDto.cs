using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace SpaceAdventures.Application.Common.Queries.Clients.GetClientsWithPagination;

public class ClientsBriefDto : IMapFrom<Client>
{
    public int IdClient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public int? IdMemberShipType { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Client, ClientsBriefDto>();
    }
}