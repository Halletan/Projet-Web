using AutoMapper;
using AutoMapper.QueryableExtensions;
using SpaceAdventures.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SpaceAdventures.Application.Common.Commands.Clients;
using SpaceAdventures.Application.Common.Exceptions;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Mappings;
using SpaceAdventures.Application.Common.Models;
using SpaceAdventures.Application.Common.Queries.Clients;
using SpaceAdventures.Application.Common.Queries.Clients.GetClientsWithPagination;

namespace SpaceAdventures.Infrastructure.Persistence.Services;

public class ClientService : IClientService
{
    private readonly ISpaceAdventureDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUsersManagementApiService _usersManagementApiService;

    public ClientService(ISpaceAdventureDbContext context, IMapper mapper, IUsersManagementApiService usersManagementApiService)
    {
        _context = context;
        _mapper = mapper;
        _usersManagementApiService = usersManagementApiService;
    }

    public async Task<ClientsVm> GetAllClients(CancellationToken cancellationToken = default)
    {
        return new ClientsVm
        {
            ClientsList = await _context.Clients
                .ProjectTo<ClientDto>(_mapper.ConfigurationProvider)
                .OrderBy(c => c.LastName)
                .ToListAsync(cancellationToken)
        };
    }

    public async Task<PaginatedList<ClientsBriefDto>> GetAllClientsWithPagination(int pageNumber, int pageSize, CancellationToken cancellation = default)
    {
        var paginatedList = await _context.Clients
            .ProjectTo<ClientsBriefDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(pageNumber, pageSize);
        return paginatedList;
    }

    public async Task<ClientDto> GetClientById(int clientId, CancellationToken cancellation = default)
    {
        var client = await _context.Clients.FindAsync(clientId);
        if (client == null) throw new NotFoundException(nameof(Client), clientId);

        return _mapper.Map<ClientDto>(client);
    }

    public async Task<ClientDto> CreateClient(ClientInput clientInput, CancellationToken cancellation = default)
    {
        var user = await _usersManagementApiService.GetAllUsers(cancellation);
        clientInput.IdUser=user.UsersList.FirstOrDefault(c=>c.Email==clientInput.Email).IdUser;

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

    public async Task<ClientDto> UpdateClient(int clientId, ClientInput clientInput,
        CancellationToken cancellation = default)
    {
        var client = await _context.Clients.FindAsync(clientId);

        if (client == null) throw new NotFoundException(nameof(Client), clientId);

        try
        {
            client.FirstName = clientInput.FirstName;
            client.LastName = clientInput.LastName;
            client.Email = clientInput.Email;

            _context.Clients.Update(client);
            await _context.SaveChangesAsync(cancellation);
            return _mapper.Map<ClientDto>(client);
        }
        catch (Exception)
        {
            throw new ValidationException();
        }
    }

    public async Task DeleteClient(int clientId, CancellationToken cancellation = default)
    {
        var client = await _context.Clients.FindAsync(clientId);

        if (client == null) throw new NotFoundException(nameof(Client), clientId);
        _context.Clients.Remove(client);
        await _context.SaveChangesAsync(cancellation);
    }

    public async Task<bool> ClientExists(string email)
    {
        return await _context.Clients.AnyAsync(c => c.Email == email);
    }

    public bool ClientExists(int? id, ClientInput clientInput)
    {
        return _context.Clients.Any(c => c.IdClient != id && c.Email == clientInput.Email);
    }
}