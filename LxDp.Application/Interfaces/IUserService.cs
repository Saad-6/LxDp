using LxDp.Domain;
using LxDp.Domain.DataModels;
using LxDp.Domain.ViewModels;

namespace LxDp.Application.Interfaces;

public interface IUserService
{
    Task<Response<User>> CreateUserAsync(CreateUserDto request);
    Task<Response<User>> UpdateUserAsync(CreateUserDto request);
    Task<Response<object>> DeleteUserAsync(int userId);
}
