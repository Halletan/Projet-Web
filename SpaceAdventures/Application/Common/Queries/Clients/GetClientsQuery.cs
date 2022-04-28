using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SpaceAdventures.Application.Common.Interfaces;

namespace SpaceAdventures.Application.Common.Queries.Clients
{
    // Query
    public class GetClientsQuery : IRequest<IEnumerable<ClientVm>> { }

    // Handler

    public class GetClientsQueryHandler : IRequestHandler<GetClientsQuery, IEnumerable<ClientVm>>
    {
        private readonly ISpaceAdventureDbContext _context;
        private readonly IMapper _mapper;

        public GetClientsQueryHandler(IMapper mapper, ISpaceAdventureDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<ClientVm>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
        {
            var clients = await _context.Clients
                .ProjectTo<ClientVm>(_mapper.ConfigurationProvider)
                .OrderBy(c => c.LastName)
                .ToListAsync(cancellationToken);

            return clients;
        }
    }
}
