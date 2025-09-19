using LxDp.Domain;
using LxDp.Domain.ViewModels;

namespace LxDp.Application.Interfaces;

public interface IScriptService
{
    Task<Response<ScriptViewModel>> CreateScriptAsync(CreateScriptDto request);
    Task<Response<ScriptViewModel>> UpdateScriptAsync(CreateScriptDto request);
    Task<Response<ScriptViewModel>> GetScriptByIdAsync(int serverId);
    Task<Response<List<ScriptViewModel>>> GetScriptsByProjectAsync(int projectId);
    Task<Response<List<ScriptViewModel>>> GetScriptsByNameAsync(string name);
    Task<Response<object>> DeleteScriptAsync(int id);
}
