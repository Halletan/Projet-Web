
using Application.Common.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SpaceAdventures.Application.Common.Commands.Clients;
using SpaceAdventures.Application.Common.Exceptions;
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

        public async Task<ClientDto> GetClientById(int clientId, CancellationToken cancellation = default)
        {
            var client = await _context.Clients.FindAsync(clientId);
            if (client == null)
            {
                throw new NotFoundException("Client", clientId);
            }

            return _mapper.Map<ClientDto>(client);
        }

        public async Task<ClientDto> CreateClient(ClientInput clientInput, CancellationToken cancellation = default)
        {
            var client = _mapper.Map<Client>(clientInput);

            try
            {
                await _context.Clients.AddAsync(client, cancellation);
                await _context.SaveChangesAsync(cancellation);
                return _mapper.Map<ClientDto>(client);
            }
            catch (Exception)   
            {
                throw new ValidationException();
            }
        }

        public async Task<ClientDto> UpdateClient(int clientId, ClientDto clientDto, CancellationToken cancellation = default)
        {
            if (!await _context.Clients.AnyAsync(c => c.IdClient == clientId, cancellationToken: cancellation))
            {
                throw new NotFoundException("Client", clientId);
            }

            try
            {
                _context.Clients.Update(_mapper.Map<Client>(clientDto));
                await _context.SaveChangesAsync(cancellation);
                return clientDto;
            }
            catch (Exception)
            {
                throw new ValidationException();
            }
        }

        public async Task DeleteClient(int clientId, CancellationToken cancellation = default)
        {
            var client = await _context.Clients.FindAsync(clientId);

            if (client == null)
            {
                _context.Clients.Remove(client);
            }
            throw new NotFoundException("Client", clientId);
        }

        public async Task<bool> ClientExists(string email)
        {
            var check = await _context.Clients.AnyAsync(c => c.Email == email);
            return check;
        }
    }
}
