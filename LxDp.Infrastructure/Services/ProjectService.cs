using LxDp.Application.Interfaces;
using LxDp.Domain;
using LxDp.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LxDp.Infrastructure.Services;

public class ProjectService : IProjectService
{
    private readonly AppDbContext _context;
    private readonly ILogger _logger;
    public ProjectService(AppDbContext context, ILogger logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task<Response<ProjectViewModel>> CreateProjectAsync(CreateProjectDto request)
    {
        try
        {
            var project = new Domain.DataModels.Project
            {
                Name = request.Name,
                PublishFolder = request.PublishFolder,
                RootDirectory = request.RootDirectory,
                Configuration = request.Configuration,
                ServerId = request.ServerId,
            };
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            var projectViewModel = new ProjectViewModel
            {
                Id = project.Id,
                Name = project.Name,
                PublishFolder = project.PublishFolder,
                RootDirectory = project.RootDirectory,
                Configuration = project.Configuration,
                ServerId = project.ServerId

            };
            _logger.LogInfo($"Project created");
            return new Response<ProjectViewModel>
            {
                Success = true,
                Message = "Project created successfully",
                Data = projectViewModel
            };

        }
        catch(Exception ex)
        {
            _logger.LogError("Error creating project", ex);
            return new Response<ProjectViewModel>
            {
                Success = false,
                Message = "Error creating project"
            };
        }
    }

    public async Task<Response<object>> DeleteProjectAsync(int id)
    {
        try
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                _logger.LogWarning($"Project with id {id} not found");
                return new Response<object>
                {
                    Success = false,
                    Message = "Project not found"
                };
            }
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            _logger.LogInfo($"Project with Id : {id} deleted");
            return new Response<object>
            {
                Success = true,
                Message = "Project deleted successfully"
            };

        }
        catch(Exception ex)
        {
            _logger.LogError("Error deleting project", ex);
            return new Response<object>
            {
                Success = false,
                Message = "Error deleting project"
            };
        }

    }

    public async Task<Response<List<ProjectViewModel>>> GetAllProjectsAsync(int serverId)
    {
        try
        {
            var projects = await _context.Projects.Include(m => m.Configuration).Where(p => p.ServerId == serverId).Select(p => new ProjectViewModel
            {
                Id = p.Id,
                Name = p.Name,
                PublishFolder = p.PublishFolder,
                RootDirectory = p.RootDirectory,
                Configuration = p.Configuration,
                ServerId = p.ServerId
            }).ToListAsync();

            _logger.LogInfo($"Retrieved {projects.Count} projects for serverId {serverId}");
            return new Response<List<ProjectViewModel>>
            {
                Success = true,
                Message = "Projects retrieved successfully",
                Data = projects
            };
        }
        catch (Exception ex)
        {
            _logger.LogError("Error retrieving projects", ex);
            return new Response<List<ProjectViewModel>>
            {
                Success = false,
                Message = "Error retrieving projects"
            };

        }
    }

    public async Task<Response<ProjectViewModel>> GetProjectByIdAsync(int id)
    {
        try
        {
            var project = await _context.Projects.Include(m => m.Configuration).FirstOrDefaultAsync(p => p.Id == id);
            if (project == null)
            {
                _logger.LogWarning($"Project with id {id} not found in GetProjectById");
                return new Response<ProjectViewModel>
                {
                    Success = false,
                    Message = "Project not found"
                };
            }
            var projectViewModel = new ProjectViewModel
            {
                Id = project.Id,
                Name = project.Name,
                PublishFolder = project.PublishFolder,
                RootDirectory = project.RootDirectory,
                Configuration = project.Configuration,
                ServerId = project.ServerId
            };
            _logger.LogInfo($"Project with Id : {id} retrieved");
            return new Response<ProjectViewModel>
            {
                Success = true,
                Message = "Project retrieved successfully",
                Data = projectViewModel
            };

        }
        catch(Exception ex)
        {
            _logger.LogError("Error retrieving project", ex);
            return new Response<ProjectViewModel>
            {
                Success = false,
                Message = "Error retrieving project"
            };
        }
    }

    public async Task<Response<ProjectViewModel>> UpdateProjectAsync(CreateProjectDto request)
    {
        try
        {
            var project = await _context.Projects.FindAsync(request.Id);
            if (project == null)
            {
                _logger.LogWarning($"Project with id {request.Id} not found in UpdateProject");
                return new Response<ProjectViewModel>
                {
                    Success = false,
                    Message = "Project not found"
                };
            }
            project.Name = request.Name;
            project.PublishFolder = request.PublishFolder;
            project.RootDirectory = request.RootDirectory;
            project.Configuration = request.Configuration;
            project.ServerId = request.ServerId;
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
            var projectViewModel = new ProjectViewModel
            {
                Id = project.Id,
                Name = project.Name,
                PublishFolder = project.PublishFolder,
                RootDirectory = project.RootDirectory,
                Configuration = project.Configuration,
                ServerId = project.ServerId
            };
            _logger.LogInfo($"Project with Id : {request.Id} updated");
            return new Response<ProjectViewModel>
            {
                Success = true,
                Message = "Project updated successfully",
                Data = projectViewModel
            };

        }
        catch(Exception ex)
        {
            _logger.LogError("Error updating project", ex);
            return new Response<ProjectViewModel>
            {
                Success = false,
                Message = "Error updating project"
            };
        }
    }
}
