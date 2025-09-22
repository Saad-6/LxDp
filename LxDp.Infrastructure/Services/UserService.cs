using LxDp.Application.Interfaces;
using LxDp.Domain;
using LxDp.Domain.DataModels;
using LxDp.Domain.ViewModels;

namespace LxDp.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    public UserService(AppDbContext context)
    {
        _context = context;
    }
    public Task<Response<User>> CreateUserAsync(CreateUserDto request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<object>> DeleteUserAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<Response<User>> UpdateUserAsync(CreateUserDto request)
    {
        throw new NotImplementedException();
    }
}
