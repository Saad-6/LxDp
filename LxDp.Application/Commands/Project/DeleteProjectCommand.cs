using LxDp.Application.Interfaces;
using LxDp.Domain;
using MediatR;

namespace LxDp.Application.Commands.Project;

public class DeleteProjectCommand : IRequest<Response<object>>
{
    public int ProjectId { get; set; }
}
public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, Response<object>>
{
    private readonly IProjectService _projectService;
    public DeleteProjectCommandHandler(IProjectService projectService)
    {
        _projectService = projectService;
    }

    public async Task<Response<object>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        return await _projectService.DeleteProjectAsync(request.ProjectId);
    }
}
