using LxDp.Application.Interfaces;
using LxDp.Domain;
using LxDp.Domain.ViewModels;
using MediatR;
using Users = LxDp.Domain.DataModels.User;
namespace LxDp.Application.Commands.User;

public class UpdateServerUserCommand : CreateUserDto, IRequest<Response<Users>>
{
}
public class UpdateServerUserCommandHandler : IRequestHandler<UpdateServerUserCommand, Response<Users>>
{
    private readonly IUserService _userService;
    public UpdateServerUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Response<Users>> Handle(UpdateServerUserCommand request, CancellationToken cancellationToken)
    {
        return await _userService.UpdateUserAsync(request);
    }
}
