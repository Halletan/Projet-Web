using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SpaceAdventures.Application.Common.Models.APIConsume;
using SpaceAdventures.Application.Common.Models.UsersAuth0ManagementApi;
using SpaceAdventures.Application.Common.Queries.Flights;
using SpaceAdventures.Application.Common.Queries.Users.Queries;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Queries.ISSCL
{
    public record ISSCLQuery : IRequest<ISSCLPosition>;

    public class ISSCLQueryHandler : IRequestHandler<ISSCLQuery, ISSCLPosition>
    {
        private readonly IISSCLService _IISSCLService;

        public ISSCLQueryHandler(IISSCLService IISSCLService)
        {
            _IISSCLService=  IISSCLService;
        }

        public async Task<ISSCLPosition> Handle(ISSCLQuery request, CancellationToken cancellationToken)
        {
            return await _IISSCLService.GetPosition(cancellationToken);
        }
    }
}


