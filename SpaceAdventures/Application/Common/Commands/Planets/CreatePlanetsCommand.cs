using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using SpaceAdventures.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Commands.Planets
{
    public class CreatePlanetsCommand : IRequest<int>
    {
        public string Name { get; set; }
    }

    public class CreatePlanetsCommandHandler : IRequestHandler<CreatePlanetsCommand, int>
    {
        private readonly ISpaceAdventureDbContext _context;
        
        public CreatePlanetsCommandHandler(ISpaceAdventureDbContext context)
        {    
            _context = context;
        }

        public async Task<int> Handle(CreatePlanetsCommand request, CancellationToken cancellationToken)
        {
            var entity = new Planet
            {
                Name = request.Name,               
            };
            
            _context.Planets.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.IdPlanet;
        }
    }
}
