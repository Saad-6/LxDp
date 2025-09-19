using LxDp.Domain;
using LxDp.Domain.ViewModels;

namespace LxDp.Application.Interfaces;

public interface IProjectService
{
    Task<Response<ProjectViewModel>> CreateProjectAsync(CreateProjectDto request);
    Task<Response<ProjectViewModel>> UpdateProjectAsync(CreateProjectDto request);
    Task<Response<List<ProjectViewModel>>> GetAllProjectsAsync(int serverId);
    Task<Response<ProjectViewModel>> GetProjectByIdAsync(int id);  
    Task<Response<object>> DeleteProjectAsync(int id);

}
