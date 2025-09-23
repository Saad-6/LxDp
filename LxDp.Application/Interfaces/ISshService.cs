using LxDp.Domain;
using LxDp.Domain.DataModels;
using LxDp.Domain.ViewModels;

namespace LxDp.Application.Interfaces;

public interface ISshService
{
    Task<bool> DirectoryExists(ServerCredentials credentials, string path);
    Task<Response<object>> ExecuteScriptAsync(ServerCredentials credentials, Script script);
    Task<Response<object>> CopyAndReplaceFileAsync(ServerCredentials credentials, FileStream file, string rootDirectory);
    Task<Response<object>> CreateDirectoryAsync(ServerCredentials credentials, string rootDirectory, string newDirectory);
    Task<Response<object>> DeleteDirectoryAsync(ServerCredentials credentials, string rootDirectory);
    Task<Response<object>> CopyDirectoryContentAsync(ServerCredentials credentials, string sourceDirectory, string destinationDirectory);
}
