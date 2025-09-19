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

public class GetAllServersQuery : IRequest<Response<List<ServerViewModel>>> 
{
}

public class GetAllSerersQueryHandler : IRequestHandler<GetAllServersQuery, Response<List<ServerViewModel>>>
{
    private readonly IServerService _serverService;
    public GetAllSerersQueryHandler(IServerService serverService)
    {
        _serverService = serverService;
    }
    public async Task<Response<List<ServerViewModel>>> Handle(GetAllServersQuery request, CancellationToken cancellationToken)
    {
        return await _serverService.GetAllServersAsync();
    }
}
