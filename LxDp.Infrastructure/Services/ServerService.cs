using LxDp.Application.Interfaces;
using LxDp.Domain;
using LxDp.Domain.DataModels;
using LxDp.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LxDp.Infrastructure.Services;

public class ServerService : IServerService
{
    private readonly AppDbContext _context;
    private readonly ILogger _logger;
    public ServerService(AppDbContext context, ILogger logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Response<ServerViewModel>> CreateServerAsync(CreateServerDto request)
    {
        try
        {
            var server = new Server
            {
                Ip = request.ServerIp,
                Name = request.ServerName,
                Port = request.ServerPort,
            };
            server.Users.Add(request.RootUser);
            if(request.Users != null && request.Users.Any())
            {
                server.Users.AddRange(request.Users);
            }

            _context.Servers.Add(server);
            await _context.SaveChangesAsync();
            var serverViewModel = new ServerViewModel
            {
                Id = server.Id,
                Name = server.Name,
                ServerPort = server.Port ?? 0,
                ServerIp = server.Ip,
                Projects = server.Projects.Select(p => new ProjectViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    PublishFolder = p.PublishFolder,
                    RootDirectory = p.RootDirectory,
                    Configuration = p.Configuration,
                    ServerId = p.ServerId
                }).ToList(),
            };
            _logger.LogInfo($"Server created");
            return new Response<ServerViewModel>
            {
                Success = true,
                Message = "Server created successfully",
                Data = serverViewModel
            };

        }
        catch (Exception ex)
        {
            _logger.LogError("Error creating server", ex);
            return new Response<ServerViewModel>
            {
                Success = false,
                Message = "Error creating server"
            };
        }
    }

    public async Task<Response<object>> DeleteServerAsync(int id)
    {
        try
        {
            var server = await _context.Servers.FindAsync(id);
            if (server == null)
            {
                _logger.LogWarning($"Server with Id: {id} not found");
                return new Response<object>
                {
                    Success = false,
                    Message = "Server not found"
                };
            }
            _context.Servers.Remove(server);
            await _context.SaveChangesAsync();
            _logger.LogInfo($"Server deleted");
            return new Response<object>
            {
                Success = true,
                Message = "Server deleted successfully"
            };

        }
        catch (Exception ex)
        {
            _logger.LogError("Error deleting server", ex);
            return new Response<object>
            {
                Success = false,
                Message = "Error deleting server"
            };
        }
    }

    public async Task<Response<List<ServerViewModel>>> GetAllServersAsync()
    {
        try
        {
            var servers = await _context.Servers.ToListAsync();
            var serverViewModels = servers.Select(s => new ServerViewModel
            {
                Id = s.Id,
                Name = s.Name,
                ServerIp = s.Ip,
                ServerPort = s.Port ?? 0,
                Projects = s.Projects.Select(p => new ProjectViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    PublishFolder = p.PublishFolder,
                    RootDirectory = p.RootDirectory,
                    Configuration = p.Configuration,
                    ServerId = p.ServerId
                }).ToList(),
            }).ToList();

            return new Response<List<ServerViewModel>>
            {
                Success = true,
                Message = "Servers retrieved successfully",
                Data = serverViewModels
            };

        }
        catch (Exception ex)
        {
            _logger.LogError("Error getting all servers", ex);
            return new Response<List<ServerViewModel>>
            {
                Success = false,
                Message = "Error getting all servers"
            };
        }
    }

    public async Task<Response<ServerViewModel>> GetServerByIdAsync(int id)
    {
        try
        {
            var server = await _context.Servers
                .Include(s => s.Projects)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (server == null)
            {
                _logger.LogWarning($"Server with Id: {id} not found");
                return new Response<ServerViewModel>
                {
                    Success = false,
                    Message = "Server not found"
                };
            }
            var serverViewModel = new ServerViewModel
            {
                Id = server.Id,
                Name = server.Name,
                ServerIp = server.Ip,
                ServerPort = server.Port ?? 0,
                Projects = server.Projects.Select(p => new ProjectViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    PublishFolder = p.PublishFolder,
                    RootDirectory = p.RootDirectory,
                    Configuration = p.Configuration,
                    ServerId = p.ServerId
                }).ToList(),
            };
            return new Response<ServerViewModel>
            {
                Success = true,
                Message = "Server retrieved successfully",
                Data = serverViewModel
            };

        }
        catch(Exception ex)
        {
            _logger.LogError("Error getting server by id", ex);
            return new Response<ServerViewModel>
            {
                Success = false,
                Message = "Error getting server by id"
            };
        }
    }

    public async Task<ServerCredentials> GetServerCredentialsAsync(int serverId)
    {
        try
        {
            var server = await _context.Servers
                .Include(s => s.Users)
                .FirstOrDefaultAsync(s => s.Id == serverId);
            if (server == null)
            {
                _logger.LogWarning($"No Server with Id : {serverId} found.");
                return null;
            }

            return new ServerCredentials
            {
                Host = server.Ip,
                Port = server.Port ?? 22,
                Username = server.Users.FirstOrDefault().UserName,
                Password = server.Users.FirstOrDefault().Password
            };

        }
        catch(Exception ex)
        {
            _logger.LogError("Error getting server credentials", ex);
            return null;
        }
    }

    public async Task<Response<ServerViewModel>> UpdateServerAsync(CreateServerDto request)
    {
        try
        {
            var server = await _context.Servers
                .Include(s => s.Users)
                .FirstOrDefaultAsync(s => s.Id == request.Id);
            if (server == null)
            {
                _logger.LogWarning($"Server with Id: {request.Id} not found");
                return new Response<ServerViewModel>
                {
                    Success = false,
                    Message = "Server not found"
                };
            }
            server.Name = request.ServerName;
            server.Ip = request.ServerIp;
            server.Port = request.ServerPort;
            // Update users
            server.Users.Clear();
            server.Users.Add(request.RootUser);
            if (request.Users != null && request.Users.Any())
            {
                server.Users.AddRange(request.Users);
            }
            await _context.SaveChangesAsync();
            var serverViewModel = new ServerViewModel
            {
                Id = server.Id,
                Name = server.Name,
                ServerIp = server.Ip,
                ServerPort = server.Port ?? 0,
                Projects = server.Projects.Select(p => new ProjectViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    PublishFolder = p.PublishFolder,
                    RootDirectory = p.RootDirectory,
                    Configuration = p.Configuration,
                    ServerId = p.ServerId
                }).ToList(),
            };
            _logger.LogInfo($"Server updated");
            return new Response<ServerViewModel>
            {
                Success = true,
                Message = "Server updated successfully",
                Data = serverViewModel
            };

        }
        catch(Exception ex)
        {
            _logger.LogError("Error updating server", ex);
            return new Response<ServerViewModel>
            {
                Success = false,
                Message = "Error updating server"
            };
        }
    }
}
