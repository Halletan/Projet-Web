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
    public class CreatePlanetValidator : AbstractValidator<CreatePlanetCommand>
    {
        private readonly ISpaceAdventureDbContext _context;

        public CreatePlanetValidator (ISpaceAdventureDbContext context)
        {
            _context = context;
            RuleFor(n => n.Name)
                .NotEmpty().WithMessage("Planet's name is mandatory")
                .MaximumLength(50).WithMessage("Planet's name should not exceed 50 characters")
                .MustAsync(BeUnique).WithMessage("This planet already exists !");
        }

        public async Task<bool> BeUnique(string name,CancellationToken cancellation)
        {
            return await _context.Planets
                .AllAsync(n => n.Name != name);
        }
    }
}
