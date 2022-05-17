using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceAdventures.Application.Common.Models.APIConsume;

namespace SpaceAdventures.Application.Common.Services.Interfaces
{
    public interface IISSCLService
    {
        Task<ISSCLPosition> GetPosition(CancellationToken cancellation = default);
    }
}
