using LxDp.Application.Interfaces;
using LxDp.Domain;
using LxDp.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LxDp.Infrastructure.Services;

public class ScriptService : IScriptService
{
    private readonly AppDbContext _context;
    private readonly ILogger _logger;
    public ScriptService(AppDbContext context, ILogger logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task<Response<ScriptViewModel>> CreateScriptAsync(CreateScriptDto request)
    {
        try
        {
            var script = new Domain.DataModels.Script
            {
                Name = request.Name,
                Description = request.Description,
                Content = request.Content,
                Order = request.Order,
                RunAfterPublishing = request.RunAfterPublishing
            };
            _context.Scripts.Add(script);
            await _context.SaveChangesAsync();
            var scriptViewModel = new ScriptViewModel
            {
                Id = script.Id,
                Name = script.Name,
                Description = script.Description,
                Content = script.Content,
                Order = script.Order,
                RunAfterPublishing = script.RunAfterPublishing
            };
            _logger.LogInfo($"Script created");
            return new Response<ScriptViewModel>
            {
                Success = true,
                Message = "Script created successfully",
                Data = scriptViewModel
            };

        }
        catch(Exception ex)
        {
            _logger.LogError("Error creating script", ex);
            return new Response<ScriptViewModel>
            {
                Success = false,
                Message = "Error creating script"
            };
        }
    }

    public async Task<Response<object>> DeleteScriptAsync(int id)
    {
        try
        {
            var script = await _context.Scripts.FindAsync(id);
            if (script == null)
            {
                _logger.LogInfo($"Script with Id: {id} not found");
                return new Response<object>
                {
                    Success = false,
                    Message = "Script not found"
                };
            }
            _context.Scripts.Remove(script);
            await _context.SaveChangesAsync();
            _logger.LogInfo($"Script deleted");
            return new Response<object>
            {
                Success = true,
                Message = "Script deleted successfully"
            };

        }
        catch(Exception ex)
        {
            _logger.LogError("Error deleting script", ex);
            return new Response<object>
            {
                Success = false,
                Message = "Error deleting script"
            };
        }
    }

    public async Task<Response<ScriptViewModel>> GetScriptByIdAsync(int serverId)
    {
        try
        {
            var script = await _context.Scripts.FindAsync(serverId);
            if (script == null)
            {
                _logger.LogInfo($"Script with Id: {serverId} not found in GetScriptByIdAsync");
                return new Response<ScriptViewModel>
                {
                    Success = false,
                    Message = "Script not found"
                };
            }
            var scriptViewModel = new ScriptViewModel
            {
                Id = script.Id,
                Name = script.Name,
                Description = script.Description,
                Content = script.Content,
                Order = script.Order,
                RunAfterPublishing = script.RunAfterPublishing
            };
            return new Response<ScriptViewModel>
            {
                Success = true,
                Message = "Script retrieved successfully",
                Data = scriptViewModel
            };

        }
        catch(Exception ex)
        {
            _logger.LogError("Error getting script by id", ex);
            return new Response<ScriptViewModel>
            {
                Success = false,
                Message = "Error getting script by id"
            };
        }
    }

    public async Task<Response<List<ScriptViewModel>>> GetScriptsByNameAsync(string name)
    {
        try
        {
            var scripts = await _context.Scripts
                .Where(s => s.Name.Contains(name))
                .ToListAsync();
            var scriptViewModels = scripts.Select(script => new ScriptViewModel
            {
                Id = script.Id,
                Name = script.Name,
                Description = script.Description,
                Content = script.Content,
                Order = script.Order,
                RunAfterPublishing = script.RunAfterPublishing
            }).ToList();
            return new Response<List<ScriptViewModel>>
            {
                Success = true,
                Message = "Scripts retrieved successfully",
                Data = scriptViewModels
            };

        }
        catch(Exception ex)
        {
            _logger.LogError("Error getting scripts by name", ex);
            return new Response<List<ScriptViewModel>>
            {
                Success = false,
                Message = "Error getting scripts by name"
            };
        }
    }

    public async Task<Response<List<ScriptViewModel>>> GetScriptsByProjectAsync(int projectId)
    {
        try
        {
            var project = await _context.Projects
                .Include(p => p.Scripts)
                .FirstOrDefaultAsync(p => p.Id == projectId);
            if (project == null)
            {
                _logger.LogInfo($"Project with Id: {projectId} not found in GetScriptsByProjectAsync");
                return new Response<List<ScriptViewModel>>
                {
                    Success = false,
                    Message = "Project not found"
                };
            }

                var scriptViewModels = project.Scripts.Select(script => new ScriptViewModel
            {
                Id = script.Id,
                Name = script.Name,
                Description = script.Description,
                Content = script.Content,
                Order = script.Order,
                RunAfterPublishing = script.RunAfterPublishing
            }).ToList();
            return new Response<List<ScriptViewModel>>
            {
                Success = true,
                Message = "Scripts retrieved successfully",
                Data = scriptViewModels
            };

        }
        catch(Exception ex)
        {
            _logger.LogError("Error getting scripts by project", ex);
            return new Response<List<ScriptViewModel>>
            {
                Success = false,
                Message = "Error getting scripts by project"
            };
        }
    }

    public async Task<Response<ScriptViewModel>> UpdateScriptAsync(CreateScriptDto request)
    {
        try
        {
            var script = await _context.Scripts.FindAsync(request.Id);
            if (script == null)
            {
                _logger.LogInfo($"Script with Id: {request.Id} not found in UpdateScriptAsync");
                return new Response<ScriptViewModel>
                {
                    Success = false,
                    Message = "Script not found"
                };
            }
            script.Name = request.Name;
            script.Description = request.Description;
            script.Content = request.Content;
            script.Order = request.Order;
            script.RunAfterPublishing = request.RunAfterPublishing;
            _context.Scripts.Update(script);
            await _context.SaveChangesAsync();
            var scriptViewModel = new ScriptViewModel
            {
                Id = script.Id,
                Name = script.Name,
                Description = script.Description,
                Content = script.Content,
                Order = script.Order,
                RunAfterPublishing = script.RunAfterPublishing
            };
            _logger.LogInfo($"Script updated");
            return new Response<ScriptViewModel>
            {
                Success = true,
                Message = "Script updated successfully",
                Data = scriptViewModel
            };

        }
        catch(Exception ex)
        {
            _logger.LogError("Error updating script", ex);
            return new Response<ScriptViewModel>
            {
                Success = false,
                Message = "Error updating script"
            };
        }
    }
}
