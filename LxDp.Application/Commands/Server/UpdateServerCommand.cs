using LxDp.Application.Interfaces;
using LxDp.Domain;
using LxDp.Domain.ViewModels;
using MediatR;

namespace LxDp.Application.Commands.Server;

public class UpdateServerCommand : CreateServerDto, IRequest<Response<ServerViewModel>>
{
}

public class UpdateServerCommandHandler : IRequestHandler<UpdateServerCommand, Response<ServerViewModel>>
{
    private readonly IServerService _serverService;
    public UpdateServerCommandHandler(IServerService serverService)
    {
        _serverService = serverService;
    }
    public async Task<Response<ServerViewModel>> Handle(UpdateServerCommand request, CancellationToken cancellationToken)
    {
        return await _serverService.UpdateServerAsync(request);
    }
}
