using LxDp.Application.Interfaces;
using LxDp.Domain;
using LxDp.Domain.ViewModels;
using MediatR;

namespace LxDp.Application.Queries.Script;

public class GetScriptsByProjectQuery : IRequest<Response<List<ScriptViewModel>>>
{
    public int ProjectId { get; set; }
}

public class GetScriptsByProjectQueryHandler : IRequestHandler<GetScriptsByProjectQuery, Response<List<ScriptViewModel>>>
{
    private readonly IScriptService _scriptService;
    public GetScriptsByProjectQueryHandler(IScriptService scriptService)
    {
        _scriptService = scriptService;
    }

    public async Task<Response<List<ScriptViewModel>>> Handle(GetScriptsByProjectQuery request, CancellationToken cancellationToken)
    {
        return await _scriptService.GetScriptsByProjectAsync(request.ProjectId);
    }
}
