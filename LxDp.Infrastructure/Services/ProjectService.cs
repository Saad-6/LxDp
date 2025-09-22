using LxDp.Application.Interfaces;
using LxDp.Domain;
using LxDp.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LxDp.Infrastructure.Services;

public class ProjectService : IProjectService
{
    private readonly AppDbContext _context;
    public ProjectService(AppDbContext context)
    {
        _context = context;
    }
    public Task<Response<ProjectViewModel>> CreateProjectAsync(CreateProjectDto request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<object>> DeleteProjectAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Response<List<ProjectViewModel>>> GetAllProjectsAsync(int serverId)
    {
        throw new NotImplementedException();
    }

    public Task<Response<ProjectViewModel>> GetProjectByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Response<ProjectViewModel>> UpdateProjectAsync(CreateProjectDto request)
    {
        throw new NotImplementedException();
    }
}
