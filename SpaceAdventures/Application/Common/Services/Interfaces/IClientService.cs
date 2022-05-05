using SpaceAdventures.Application.Common.Queries.Clients;

namespace Application.Common.Services.Interfaces;       

public interface IClientService 
{
    Task<ClientsVm> GetAllClients(CancellationToken cancellation);
}