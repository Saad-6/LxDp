using LxDp.Domain;
using LxDp.Domain.DataModels;

namespace LxDp.Application.Interfaces;

public interface ISshService
{
    Task<Response<object>> InitializeConnectionAsync(Server server);
    Task<Response<string>> ExecuteScriptAsync(Script script);
    Task<Response<string>> CopyAndReplaceFileAsync(FileStream file, string rootDirectory);
    Task<Response<string>> CreateDirectoryAsync(string rootDirectory, string newDirectory);
    Task<Response<string>> DeleteDirectoryAsync(string rootDirectory);
    Task<Response<string>> CopyDirectoryContentAsync(string sourceDirectory, string destinationDirectory);
    Task<Response<object>> TerminateConnectionAsync();
}
