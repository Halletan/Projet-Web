using MediatR;
using SpaceAdventures.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Queries.Users.Queries
{
    // Query
    public class GetUsersQuery : IRequest<UsersVm>
    {
    }

    // Handler
    public class GetUserssQueryHandler : IRequestHandler<GetUsersQuery, UsersVm>
    {
        private readonly IUsersManagementApiService _UsersManagementApiService;

        public GetUserssQueryHandler(IUsersManagementApiService usersManagementApiService)
        {
            _UsersManagementApiService = usersManagementApiService;
        }

        public async Task<UsersVm> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return await _UsersManagementApiService.GetAllUsers(cancellationToken);
        }
    }
}
