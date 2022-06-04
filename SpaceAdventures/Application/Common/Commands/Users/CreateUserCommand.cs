using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Queries.Users.Queries;

namespace SpaceAdventures.Application.Common.Commands.Users
{
    public record CreateUserCommand (UserInput UserInput): IRequest<UserDto>;
    

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IUsersManagementApiService _userService;


        public CreateUserCommandHandler(IUsersManagementApiService userService)
        {
            _userService = userService;
        }

        public async Task<UserDto> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            return await _userService.CreateUser(command.UserInput , cancellationToken);
        }
    }
}
