using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SpaceAdventures.Application.Common.Commands.Planets;
using SpaceAdventures.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Validations.Planets
{
    public class CreatePlanetValidation : AbstractValidator<CreatePlanetsCommand>
    {
        private readonly ISpaceAdventureDbContext _context;

        public CreatePlanetValidation (ISpaceAdventureDbContext context)
        {
            _context = context;
            RuleFor(n => n.Name)
                .NotEmpty().WithMessage("The name of the planet is mandatory")
                .MaximumLength(50).WithMessage("The planet name must not exceed 50 characters")
                .MustAsync(BeUnique).WithMessage("The planet already exist !");
        }

        public async Task<bool> BeUnique(string name,CancellationToken cancellation)
        {
            return await _context.Planets
                .AllAsync(n => n.Name != name);
        }
    }
}
