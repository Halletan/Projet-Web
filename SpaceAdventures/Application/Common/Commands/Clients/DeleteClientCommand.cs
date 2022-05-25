using Application.Common.Services.Interfaces;
using MediatR;

namespace SpaceAdventures.Application.Common.Commands.Clients;

public record DeleteClientCommand(int Id) : IRequest;

public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand>
{
    private readonly IClientService _clientService;

    public DeleteClientCommandHandler(IClientService clientService)
    {
        _clientService = clientService;
    }

    public async Task<Unit> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        await _clientService.DeleteClient(request.Id, cancellationToken);
        return Unit.Value;
    }
}