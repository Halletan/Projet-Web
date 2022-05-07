using SpaceAdventures.Application.Common.Commands.Clients;
using SpaceAdventures.Application.Common.Queries.Clients;

namespace Application.Common.Services.Interfaces;       

public interface IClientService 
{
    Task<ClientsVm> GetAllClients(CancellationToken cancellation = default);
    Task<ClientDto> GetClientById(int clientId, CancellationToken cancellation = default);
    Task<ClientDto> CreateClient(ClientInput clientInput, CancellationToken cancellation = default);    
    Task<ClientDto> UpdateClient(int clientId, ClientDto clientDto, CancellationToken cancellation = default);
    Task DeleteClient(int clientId, CancellationToken cancellation = default);
    Task<bool> ClientExists(string email);    


}