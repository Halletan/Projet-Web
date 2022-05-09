using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Queries.Planets
{
    public class PlanetVm : IMapFrom<Planet>    
    {
        public IList<PlanetDto> PlanetsList { get; set; }
    }
}
