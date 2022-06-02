using MediatR;
using SpaceAdventures.Application.Common.Queries.Users.Queries;
using SpaceAdventures.Application.Common.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAdventures.Application.Common.Commands.Users
{
    public record UpdateUserCommand(UserInput UserInput) : IRequest<UserDto>;


    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
    {
        private readonly IUsersManagementApiService _userService;


        public UpdateUserCommandHandler(IUsersManagementApiService userService)
        {
            _userService = userService;
        }

        public async Task<UserDto> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            return await _userService.UpdateUser(command.UserInput, cancellationToken);
        }
    }
}
