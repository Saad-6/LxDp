using LxDp.Application.Interfaces;
using LxDp.Domain;
using LxDp.Domain.ViewModels;
using MediatR;

namespace LxDp.Application.Commands.Script;

public class UpdateScriptCommand : CreateScriptDto, IRequest<Response<ScriptViewModel>>
{
}
public class UpdateScriptCommandHandler : IRequestHandler<UpdateScriptCommand, Response<ScriptViewModel>>
{
    private readonly IScriptService _scriptService;
    public UpdateScriptCommandHandler(IScriptService scriptService)
    {
        _scriptService = scriptService;
    }

    public async Task<Response<ScriptViewModel>> Handle(UpdateScriptCommand request, CancellationToken cancellationToken)
    {
        return await _scriptService.UpdateScriptAsync(request);
    }
}
