using MediatR;
using SpaceAdventures.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Queries.Users.Queries
{
    public record GetUserByIdQuery(int Id) : IRequest<UserDto>;

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUsersManagementApiService _usersManagementApiService;

        public GetUserByIdQueryHandler(IUsersManagementApiService usersManagementApiService)
        {
            _usersManagementApiService = usersManagementApiService;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _usersManagementApiService.GetUserById(request.Id, cancellationToken);
        }

    }
}
