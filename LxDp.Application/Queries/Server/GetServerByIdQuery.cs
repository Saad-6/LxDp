using LxDp.Application.Interfaces;
using LxDp.Domain;
using LxDp.Domain.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LxDp.Application.Queries.Server;

public class GetServerByIdQuery : IRequest<Response<ServerViewModel>>
{
    public int Id { get; set; }
}

public class GetServerByIdQueryHandler : IRequestHandler<GetServerByIdQuery, Response<ServerViewModel>>
{
    private readonly IServerService _serverService;
    public GetServerByIdQueryHandler(IServerService serverService)
    {
        _serverService = serverService;
    }
    async Task<Response<ServerViewModel>> IRequestHandler<GetServerByIdQuery, Response<ServerViewModel>>.Handle(GetServerByIdQuery request, CancellationToken cancellationToken)
    {
        return await _serverService.GetServerByIdAsync(request.Id);
    }
}
