using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SpaceAdventures.Application.Common.Queries.Users.Queries;
using SpaceAdventures.Application.Common.Services.Interfaces;

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
