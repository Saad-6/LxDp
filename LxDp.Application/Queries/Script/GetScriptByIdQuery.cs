using LxDp.Application.Interfaces;
using LxDp.Domain;
using LxDp.Domain.ViewModels;
using MediatR;

namespace LxDp.Application.Queries.Script;

public class GetScriptByIdQuery : IRequest<Response<ScriptViewModel>>
{
    public int ScriptId { get; set; }
}
public class GetScriptByIdQueryHandler : IRequestHandler<GetScriptByIdQuery, Response<ScriptViewModel>>
{
    private readonly IScriptService _scriptService;
    public GetScriptByIdQueryHandler(IScriptService scriptService)
    {
        _scriptService = scriptService;
    }

    public async Task<Response<ScriptViewModel>> Handle(GetScriptByIdQuery request, CancellationToken cancellationToken)
    {
        return await _scriptService.GetScriptByIdAsync(request.ScriptId);
    }
}