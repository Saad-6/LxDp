using LxDp.Application.Interfaces;
using LxDp.Domain;
using MediatR;

namespace LxDp.Application.Commands.Server;

public class DeleteServerCommand : IRequest<Response<object>>
{
    public int ServerId { get; set; }
}

public class DeleteServerCommandHandler : IRequestHandler<DeleteServerCommand, Response<object>>
{
    private readonly IServerService _serverService;
    public DeleteServerCommandHandler(IServerService serverService)
    {
        _serverService = serverService;
    }

    public async Task<Response<object>> Handle(DeleteServerCommand request, CancellationToken cancellationToken)
    {
        return await _serverService.DeleteServerAsync(request.ServerId);
    }
}
