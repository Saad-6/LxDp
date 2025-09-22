using LxDp.Application.Interfaces;
using LxDp.Domain;
using LxDp.Domain.ViewModels;

namespace LxDp.Infrastructure.Services;

public class ScriptService : IScriptService
{
    private readonly AppDbContext _context;
    public ScriptService(AppDbContext context)
    {
        _context = context;
    }
    public Task<Response<ScriptViewModel>> CreateScriptAsync(CreateScriptDto request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<object>> DeleteScriptAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Response<ScriptViewModel>> GetScriptByIdAsync(int serverId)
    {
        throw new NotImplementedException();
    }

    public Task<Response<List<ScriptViewModel>>> GetScriptsByNameAsync(string name)
    {
        throw new NotImplementedException();
    }

    public Task<Response<List<ScriptViewModel>>> GetScriptsByProjectAsync(int projectId)
    {
        throw new NotImplementedException();
    }

    public Task<Response<ScriptViewModel>> UpdateScriptAsync(CreateScriptDto request)
    {
        throw new NotImplementedException();
    }
}
