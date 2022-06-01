using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SpaceAdventures.Application.Common.Models.UsersAuth0ManagementApi;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Queries.Users.Queries
{
    public record GetUserByEmailQuery(string email) : IRequest<UserDto>;
    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, UserDto>
    {
        private readonly IUsersManagementApiService _usersManagementApiService;

        public GetUserByEmailQueryHandler(IUsersManagementApiService usersManagementApiService)
        {
            _usersManagementApiService = usersManagementApiService;
        }

        public async Task<UserDto> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            return await _usersManagementApiService.GetUserByEmail(request.email, cancellationToken);
        }
    }

}
