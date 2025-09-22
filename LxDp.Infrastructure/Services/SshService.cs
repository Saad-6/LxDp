using LxDp.Application.Interfaces;
using LxDp.Domain;
using LxDp.Domain.DataModels;

namespace LxDp.Infrastructure.Services;

public class SshService : ISshService
{
    public Task<Response<string>> CopyAndReplaceFileAsync(FileStream file, string rootDirectory)
    {
        throw new NotImplementedException();
    }

    public Task<Response<string>> CopyDirectoryContentAsync(string sourceDirectory, string destinationDirectory)
    {
        throw new NotImplementedException();
    }

    public Task<Response<string>> CreateDirectoryAsync(string rootDirectory, string newDirectory)
    {
        throw new NotImplementedException();
    }

    public Task<Response<string>> DeleteDirectoryAsync(string rootDirectory)
    {
        throw new NotImplementedException();
    }

    public Task<Response<string>> ExecuteScriptAsync(Script script)
    {
        throw new NotImplementedException();
    }

    public Task<Response<object>> InitializeConnectionAsync(Server server)
    {
        throw new NotImplementedException();
    }

    public Task<Response<object>> TerminateConnectionAsync()
    {
        throw new NotImplementedException();
    }
}
