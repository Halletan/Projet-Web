using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SpaceAdventures.Application.Common.Services.Interfaces;

namespace SpaceAdventures.Application.Common.Commands.Users
{
    public record DeleteUserCommand(int Id) : IRequest;

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUsersManagementApiService _usersManagementApiService;

        public DeleteUserCommandHandler(IUsersManagementApiService usersManagementApiService)
        {
             _usersManagementApiService=usersManagementApiService;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _usersManagementApiService.DeleteUser(request.Id, cancellationToken);
            return Unit.Value;
        }
    }
}
