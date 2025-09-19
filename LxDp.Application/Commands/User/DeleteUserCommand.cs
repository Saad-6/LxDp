using LxDp.Application.Interfaces;
using LxDp.Domain;
using MediatR;
namespace LxDp.Application.Commands.User;

public class DeleteUserCommand : IRequest<Response<object>>
{
    public int UserId { get; set; }
}

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Response<object>>
{
    private readonly IUserService _userService;
    public DeleteUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Response<object>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        return await _userService.DeleteUserAsync(request.UserId);
    }
}