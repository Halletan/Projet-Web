﻿using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using SpaceAdventures.Application.Common.Queries.Clients;

namespace SpaceAdventures.Application.Common.Queries.Membership;

public class MembershipDto : IMapFrom<MembershipType>
{
    public int IdMemberShipType { get; set; }
    public string Name { get; set; }
    public double DiscountFactor { get; set; }

    public virtual ICollection<ClientDto> Clients { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<MembershipType, MembershipDto>().ReverseMap();
    }
}