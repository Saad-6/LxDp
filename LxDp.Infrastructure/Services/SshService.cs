using LxDp.Application.Interfaces;
using LxDp.Domain;
using LxDp.Domain.DataModels;
using LxDp.Domain.ViewModels;
using Renci.SshNet;
using System.Text;

namespace LxDp.Infrastructure.Services;

public class SshService : ISshService
{
    private readonly ILogger _logger;
    public SshService(ILogger logger)
    {
        _logger = logger;
    }
    // MAJOR BUG NEED RESOLVING 
    public async Task<Response<object>> CopyAndReplaceFileAsync(ServerCredentials credentials, FileStream file, string rootDirectory)
    {
        try
        {
            using (var client = new SftpClient(credentials.Host, credentials.Username, credentials.Password))
            {
                await client.ConnectAsync(default);

                client.UploadFile(file, rootDirectory);

                client.Disconnect();

            }
            return new Response<object> { Message = "File copied and replaced successfully via SFTP" };

        }
        catch (Exception ex)
        {
            _logger.LogError("Error copying and replacing file via SFTP", ex);
            return new Response<object> { Message = "Error copying and replacing file via SFTP" };
        } 

    }

    public async Task<Response<object>> CopyDirectoryContentAsync(ServerCredentials credentials, string sourceDirectory, string destinationDirectory)
    {
        try
        {
            using (var client = new SftpClient(credentials.Host, credentials.Username, credentials.Password))
            {
                await client.ConnectAsync(default);

                if (!client.Exists(destinationDirectory))
                {
                    client.CreateDirectory(destinationDirectory);
                }

                // Get all files and directories in source
                var items = client.ListDirectory(sourceDirectory);

                foreach (var item in items)
                {
                    if (item.Name == "." || item.Name == "..")
                        continue;

                    string sourcePath = $"{sourceDirectory.TrimEnd('/')}/{item.Name}";
                    string destPath = $"{destinationDirectory.TrimEnd('/')}/{item.Name}";

                    if (item.IsDirectory)
                    {
                        // Recursively copy directory
                        var result = await CopyDirectoryContentAsync(credentials, sourcePath, destPath);
                        if (!string.IsNullOrEmpty(result.Message) && result.Message.Contains("Error"))
                        {
                            client.Disconnect();
                            return new Response<object> { Message = $"Error copying directory content: {result.Message}" };
                        }
                    }
                    else
                    {
                        // Copy file
                        using (var sourceStream = client.OpenRead(sourcePath))
                        using (var destStream = client.Create(destPath))
                        {
                            await sourceStream.CopyToAsync(destStream);
                        }
                    }
                }

                client.Disconnect();
                _logger.LogInfo($"Directory content copied successfully from {sourceDirectory} to {destinationDirectory}");

            }
            return new Response<object> { Message = "File copied and replaced successfully via SFTP" };

        }
        catch (Exception ex)
        {
            _logger.LogError("Error copying and replacing file via SFTP", ex);
            return new Response<object> { Message = "Error copying and replacing file via SFTP" };
        }
    }

    public async Task<Response<object>> CreateDirectoryAsync(ServerCredentials credentials, string rootDirectory, string newDirectory)
    {
        try
        {
            using (var client = new SftpClient(credentials.Host, credentials.Username, credentials.Password))
            {
                await client.ConnectAsync(default);

                string fullPath = $"{rootDirectory.TrimEnd('/')}/{newDirectory}";

                if (client.Exists(fullPath))
                {
                    client.Disconnect();
                    return new Response<object> { Message = "Directory already exists" };
                }

                client.CreateDirectory(fullPath);
                client.Disconnect();

                _logger.LogInfo($"Directory created successfully: {fullPath}");
                return new Response<object> { Message = "Directory created successfully" };
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error creating directory {newDirectory} in {rootDirectory}", ex);
            return new Response<object> { Message = $"Error creating directory: {ex.Message}" };
        }
    }

    public async Task<Response<object>> DeleteDirectoryAsync(ServerCredentials credentials, string rootDirectory)
    {
        try
        {
            using (var client = new SftpClient(credentials.Host, credentials.Username, credentials.Password))
            {
                await client.ConnectAsync(default);

                string fullPath = rootDirectory;

                if (!client.Exists(fullPath))
                {
                    client.Disconnect();
                    return new Response<object> { Message = "Directory does not exists" };
                }

                await client.DeleteDirectoryAsync(fullPath);
                client.Disconnect();

                _logger.LogInfo($"Directory deleted successfully: {fullPath}");
                return new Response<object> { Message = "Directory deleted successfully" };
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error deleting directory {rootDirectory}", ex);
            return new Response<object> { Message = $"Error creating directory: {ex.Message}" };
        }
    }

    public async Task<bool> DirectoryExists(ServerCredentials credentials, string path)
    {
        try
        {
            bool dirExists;
            using (var client = new SftpClient(credentials.Host, credentials.Username, credentials.Password))
            {
                
                await client.ConnectAsync(default);
                dirExists = client.Exists(path);
                client.Disconnect();
            }
           
            return dirExists;

        }
        catch(Exception ex)
        {
            _logger.LogError($"Directory exists operation for {path} failed with error :", ex);
            return false;
        }
    }

    public async Task<Response<object>> ExecuteScriptAsync(ServerCredentials credentials, Script script)
    {
        try
        {
            using (var client = new SshClient(credentials.Host, credentials.Username, credentials.Password))
            {
                await client.ConnectAsync(default);

                var command = client.CreateCommand(script.Content);
                var result = command.Execute();

                var response = new StringBuilder();
                response.AppendLine($"Exit Status: {command.ExitStatus}");

                if (!string.IsNullOrEmpty(result))
                {
                    response.AppendLine("Output:");
                    response.AppendLine(result);
                }

                if (!string.IsNullOrEmpty(command.Error))
                {
                    response.AppendLine("Error:");
                    response.AppendLine(command.Error);
                }

                client.Disconnect();

                _logger.LogInfo($"Script executed successfully. Exit status: {command.ExitStatus}");
                return new Response<object>
                {
                    Message = command.ExitStatus == 0 ? "Script executed successfully" : "Script executed with errors",
                    Data = response.ToString()
                };
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Error executing script", ex);
            return new Response<object> { Message = $"Error executing script: {ex.Message}" };
        }
    }
}
