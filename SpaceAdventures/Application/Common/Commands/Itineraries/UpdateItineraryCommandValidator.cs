using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Services.Interfaces;
using FluentValidation;
using SpaceAdventures.Application.Common.Commands.Airports;

namespace SpaceAdventures.Application.Common.Commands.Itineraries
{
    public class UpdateItineraryCommandValidator : AbstractValidator<UpdateItineraryCommand>
    {
     
    }
}
