using LxDp.Application.Interfaces;
using LxDp.Domain;
using LxDp.Domain.ViewModels;
using MediatR;

namespace LxDp.Application.Commands.Script;

public class CreateScriptCommand : CreateScriptDto, IRequest<Response<ScriptViewModel>>
{
}
public class CreateScriptCommandHandler : IRequestHandler<CreateScriptCommand, Response<ScriptViewModel>>
{
    private readonly IScriptService _scriptService;
    public CreateScriptCommandHandler(IScriptService scriptService)
    {
        _scriptService = scriptService;
    }

    public async Task<Response<ScriptViewModel>> Handle(CreateScriptCommand request, CancellationToken cancellationToken)
    {
        return await _scriptService.CreateScriptAsync(request);
    }
}