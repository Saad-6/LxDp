using LxDp.Application.Interfaces;
using LxDp.Domain;
using LxDp.Domain.ViewModels;
using MediatR;

namespace LxDp.Application.Commands.Server;

public class CreateServerCommand : CreateServerDto, IRequest<Response<ServerViewModel>>
{
}

public class CreateServerCommandHandler : IRequestHandler<CreateServerCommand, Response<ServerViewModel>>
{
    private readonly IServerService _serverService;
    public CreateServerCommandHandler(IServerService serverService)
    {
        _serverService = serverService;
    }
    public async Task<Response<ServerViewModel>> Handle(CreateServerCommand request, CancellationToken cancellationToken)
    {
        return await _serverService.CreateServerAsync(request);
    }
}