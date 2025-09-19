using LxDp.Application.Interfaces;
using LxDp.Domain;
using LxDp.Domain.ViewModels;
using MediatR;
using Users = LxDp.Domain.DataModels.User;

namespace LxDp.Application.Commands.User;

public class CreateServerUserCommand : CreateUserDto, IRequest<Response<Users>>
{
}
public class CreateServerUserCommandHandler : IRequestHandler<CreateServerUserCommand, Response<Users>>
{
    private readonly IUserService _userService;
    public CreateServerUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Response<Users>> Handle(CreateServerUserCommand request, CancellationToken cancellationToken)
    {
        return await _userService.CreateUserAsync(request);
    }
}