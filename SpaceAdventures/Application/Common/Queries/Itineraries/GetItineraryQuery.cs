using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Services;
using Application.Common.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Queries.Itineraries
{
    // Query
    public class GetItineraryQuery : IRequest<ItineraryVm> { }

    // Handler
    public class GetItinerarysQueryHandler : IRequestHandler<GetItineraryQuery, ItineraryVm>
    {
        private readonly IItineraryService _ItineraryService;

        public GetItinerarysQueryHandler(IItineraryService ItineraryService)
        {
            _ItineraryService = ItineraryService;
        }

        public async Task<ItineraryVm> Handle(GetItineraryQuery request, CancellationToken cancellationToken)
        {
            return await _ItineraryService.GetAllItineraries(cancellationToken);  
        }
    }
}
