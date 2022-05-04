using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace SpaceAdventures.Application.Common.Queries.Clients
{
    public class ClientsVm
    {
        public IList<ClientDto> ClientsList { get; set; }
    }
}
