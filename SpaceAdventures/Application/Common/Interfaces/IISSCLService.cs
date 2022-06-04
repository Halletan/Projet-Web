using SpaceAdventures.Application.Common.Models.APIConsume;

namespace SpaceAdventures.Application.Common.Interfaces;

public interface IISSCLService
{
    Task<ISSCLPosition> GetPosition(CancellationToken cancellation = default);
}