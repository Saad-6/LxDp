using LxDp.Application.Interfaces;
using LxDp.Domain;
using LxDp.Domain.ViewModels;
using MediatR;

namespace LxDp.Application.Commands.Project;

public class UpdateProjectCommand : CreateProjectDto, IRequest<Response<ProjectViewModel>>
{
}
public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Response<ProjectViewModel>>
{
    private readonly IProjectService _projectService;
    public UpdateProjectCommandHandler(IProjectService projectService)
    {
        _projectService = projectService;
    }
    public async Task<Response<ProjectViewModel>> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        return await _projectService.UpdateProjectAsync(request);
    }
}