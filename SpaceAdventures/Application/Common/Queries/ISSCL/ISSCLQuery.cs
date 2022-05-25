using MediatR;
using SpaceAdventures.Application.Common.Models.APIConsume;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Queries.ISSCL;

public record ISSCLQuery : IRequest<ISSCLPosition>;

public class ISSCLQueryHandler : IRequestHandler<ISSCLQuery, ISSCLPosition>
{
    private readonly IISSCLService _IISSCLService;

    public ISSCLQueryHandler(IISSCLService IISSCLService)
    {
        _IISSCLService = IISSCLService;
    }

    public async Task<ISSCLPosition> Handle(ISSCLQuery request, CancellationToken cancellationToken)
    {
        return await _IISSCLService.GetPosition(cancellationToken);
    }
}