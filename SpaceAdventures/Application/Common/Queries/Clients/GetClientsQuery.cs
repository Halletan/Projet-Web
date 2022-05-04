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
    public class GetClientsQuery : IRequest<ClientsVm> { }

    // Handler

    public class GetClientsQueryHandler : IRequestHandler<GetClientsQuery, ClientsVm>
    {
        private readonly ISpaceAdventureDbContext _context;
        private readonly IMapper _mapper;

        public GetClientsQueryHandler(IMapper mapper, ISpaceAdventureDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ClientsVm> Handle(GetClientsQuery request, CancellationToken cancellationToken)
        {
            return new ClientsVm
            {
                ClientsList = await _context.Clients
                    .ProjectTo<ClientDto>(_mapper.ConfigurationProvider)
                    .OrderBy(c => c.LastName)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
