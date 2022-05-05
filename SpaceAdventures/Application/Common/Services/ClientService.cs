
using Application.Common.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Queries.Clients;

namespace SpaceAdventures.Application.Common.Services
{
    public class ClientService : IClientService
    {
        private readonly ISpaceAdventureDbContext _context;
        private readonly IMapper _mapper;

        public ClientService(ISpaceAdventureDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ClientsVm> GetAllClients(CancellationToken cancellationToken)    
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
