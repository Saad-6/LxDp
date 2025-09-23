using LxDp.Application.Interfaces;
using LxDp.Domain;
using LxDp.Domain.DataModels;
using LxDp.Domain.ViewModels;

namespace LxDp.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly ILogger _logger;
    public UserService(AppDbContext context, ILogger logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task<Response<User>> CreateUserAsync(CreateUserDto request)
    {
        try
        {
            var server = _context.Servers.FirstOrDefault(s => s.Id == request.Server.Id);
            if(server == null)
            {
                _logger.LogWarning($"Server with Id: {request.Server.Id} not found");
                return new Response<User>
                {
                    Success = false,
                    Message = "Server not found"
                };
            }

            var user = new User
            {
                UserName = request.User.UserName,
                Password = request.User.Password,
                Server = server
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new Response<User>
            {
                Success = true,
                Message = "User created successfully",
                Data = user
            };

        }
        catch(Exception ex)
        {
            return new Response<User>
            {
                Success = false,
                Message = "Error creating user"
            };
        }
    }

    public async Task<Response<object>> DeleteUserAsync(int userId)
    {
        try
        {
            var server = _context.Servers.FirstOrDefault(s => s.Users.Any(u => u.Id == userId));
            if(server == null)
            {
                _logger.LogWarning($"User with Id: {userId} not found");
                return new Response<object>
                {
                    Success = false,
                    Message = "User not found"
                };
            }
            var user = server.Users.FirstOrDefault(u => u.Id == userId);
            if(user == null)
            {
                _logger.LogWarning($"User with Id: {userId} not found");
                return new Response<object>
                {
                    Success = false,
                    Message = "User not found"
                };
            }
            server.Users.Remove(user);
            await _context.SaveChangesAsync();
            return new Response<object>
            {
                Success = true,
                Message = "User deleted successfully"
            };

        }
        catch(Exception ex)
        {
            return new Response<object>
            {
                Success = false,
                Message = "Error deleting user"
            };
        }
    }

    public async Task<Response<User>> UpdateUserAsync(CreateUserDto request)
    {
        try
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == request.User.Id);
            if(user == null)
            {
                _logger.LogWarning($"User with Id: {request.User.Id} not found");
                return new Response<User>
                {
                    Success = false,
                    Message = "User not found"
                };
            }
            user.UserName = request.User.UserName;
            user.Password = request.User.Password;
            await _context.SaveChangesAsync();
            return new Response<User>
            {
                Success = true,
                Message = "User updated successfully",
                Data = user
            };

        }
        catch(Exception ex)
        {
            return new Response<User>
            {
                Success = false,
                Message = "Error updating user"
            };
        }
    }
}
