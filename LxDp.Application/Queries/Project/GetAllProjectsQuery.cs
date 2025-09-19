using LxDp.Application.Interfaces;
using LxDp.Domain;
using LxDp.Domain.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LxDp.Application.Queries.Project;

public class GetAllProjectsQuery : IRequest<Response<List<ProjectViewModel>>>
{
    public int ServerId { get; set; }
}

public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, Response<List<ProjectViewModel>>>
{
    private readonly IProjectService _projectService;

    public GetAllProjectsQueryHandler(IProjectService projectService)
    {
        _projectService = projectService;
    }
    public async Task<Response<List<ProjectViewModel>>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
    {
        return await _projectService.GetAllProjectsAsync(request.ServerId);
    }
}
