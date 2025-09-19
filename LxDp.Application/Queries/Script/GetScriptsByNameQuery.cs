using LxDp.Application.Interfaces;
using LxDp.Domain;
using LxDp.Domain.ViewModels;
using MediatR;

namespace LxDp.Application.Queries.Script;

public class GetScriptsByNameQuery : IRequest<Response<List<ScriptViewModel>>>
{
    public string Name { get; set; }
}
public class GetScriptByNameQueryHandler : IRequestHandler<GetScriptsByNameQuery, Response<List<ScriptViewModel>>>
{
    private readonly IScriptService _scriptService;
    public GetScriptByNameQueryHandler(IScriptService scriptService)
    {
        _scriptService = scriptService;
    }

    public async Task<Response<List<ScriptViewModel>>> Handle(GetScriptsByNameQuery request, CancellationToken cancellationToken)
    {
        return await _scriptService.GetScriptsByNameAsync(request.Name);
    }
}
