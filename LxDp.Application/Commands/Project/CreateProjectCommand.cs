using LxDp.Application.Interfaces;
using LxDp.Domain;
using LxDp.Domain.ViewModels;
using MediatR;

namespace LxDp.Application.Commands.Project;

public class CreateProjectCommand : CreateProjectDto, IRequest<Response<ProjectViewModel>>
{
}
public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Response<ProjectViewModel>>
{
    private readonly IProjectService _projectService;

    public CreateProjectCommandHandler(IProjectService projectService)
    {
        _projectService = projectService;
    }

    public async Task<Response<ProjectViewModel>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        return await _projectService.CreateProjectAsync(request);
    }
}
