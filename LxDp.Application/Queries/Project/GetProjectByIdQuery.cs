using LxDp.Application.Interfaces;
using LxDp.Domain;
using LxDp.Domain.ViewModels;
using MediatR;

namespace LxDp.Application.Queries.Project;

public class GetAllProjectByIdQuery : IRequest<Response<ProjectViewModel>>
{
    public int Id { get; set; }
}

public class GetAllProjectByIdQueryHandler : IRequestHandler<GetAllProjectByIdQuery, Response<ProjectViewModel>>
{
    private readonly IProjectService _projectService;

    public GetAllProjectByIdQueryHandler(IProjectService projectService)
    {
        _projectService = projectService;
    }
    public async Task<Response<ProjectViewModel>> Handle(GetAllProjectByIdQuery request, CancellationToken cancellationToken)
    {
        return await _projectService.GetProjectByIdAsync(request.Id);
    }
}