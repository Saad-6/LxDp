using LxDp.Application.Interfaces;
using LxDp.Domain;
using MediatR;

namespace LxDp.Application.Commands.Script;

public class DeleteScriptCommand : IRequest<Response<object>>
{
    public int ScriptId { get; set; }
}
public class DeleteScriptCommandHandler : IRequestHandler<DeleteScriptCommand, Response<object>>
{
    private readonly IScriptService _scriptService;
    public DeleteScriptCommandHandler(IScriptService scriptService)
    {
        _scriptService = scriptService;
    }

    public async Task<Response<object>> Handle(DeleteScriptCommand request, CancellationToken cancellationToken)
    {
        return await _scriptService.DeleteScriptAsync(request.ScriptId);
    }
}
