using LxDp.Application.Interfaces;
using LxDp.Domain;
using LxDp.Domain.DataModels;
using LxDp.Domain.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;

namespace LxDp.Infrastructure.Services;

public class PublishService : IPublishService
{
    private readonly ISshService _secureShell;
    private readonly IServerService _serverService;
    private readonly ILogger _logger;
    private const string Backup_Prefix = "bk";
    public PublishService(ISshService secureShell, IServerService serverService, ILogger logger)
    {
        _secureShell = secureShell;
        _serverService = serverService;
        _logger = logger;
    }
    public async Task<Response<string>> PublishAsync(PublishModel request)
    {
        try
        {
            _logger.LogInfo($"Publishing project started | Project name : {request.Project.Name}");
            bool firstPublish = false;

            // 1. Create SSH Creds Model
            
            var credentials = await _serverService.GetServerCredentialsAsync(request.Project.ServerId);
            if(credentials == null) throw new Exception("Server credentials not found");

            // 2. Check if any scripts need to be run BEFORE publishing, if so, run them in order
            
            var scriptsBeforePublishing = request.Project.Scripts.Where(m=>m.RunAfterPublishing == false).ToList();
            await RunScriptsAsync(scriptsBeforePublishing, credentials);

            // 3. Check if publish folder (project name) exists on server

            var publishFolderPath = $"{request.Project.RootDirectory.TrimEnd('/')}/{request.Project.PublishFolder}";

            if(!await _secureShell.DirectoryExists(credentials, publishFolderPath))
            {
                firstPublish = true;
                await _secureShell.CreateDirectoryAsync(credentials, request.Project.RootDirectory, request.Project.PublishFolder);
            }

            // 4. Check if backup folder exists, if not, create it, move it's content to the backup folder, if it doesn't, create it

            var backupFolderPath = $"{Backup_Prefix}_{publishFolderPath}";
            if(!await _secureShell.DirectoryExists(credentials, backupFolderPath))
            {
                await _secureShell.CreateDirectoryAsync(credentials, request.Project.RootDirectory, $"{Backup_Prefix}_{request.Project.PublishFolder}");
            }

            // 5. Create backup if its not a firstpublish
            if (!firstPublish)
            {
                await _secureShell.CopyDirectoryContentAsync(credentials, publishFolderPath, backupFolderPath);
            }

            // 6. Move the files in the publish folder

            await CopyAndReplaceFilesAsync(credentials, request.Files, publishFolderPath);

            // 7. If it's a first publish, create backup
            if (firstPublish)
            {
                await _secureShell.CopyDirectoryContentAsync(credentials, publishFolderPath, backupFolderPath);
            }


            // 6. Check if any scripts need to be run AFTER publishing, if so, run them in order
            var scriptsAfterPublish = request.Project.Scripts.Where(m=>m.RunAfterPublishing == true).ToList();
            await RunScriptsAsync(scriptsAfterPublish, credentials);

            _logger.LogInfo($"Published {request.Project.Name} successfully");
            return new Response<string> { Message = "Publish successfull"};

        }
        catch (Exception ex)
        {
            _logger.LogError("Error publishing project", ex);
            return new Response<string>
            {
                Success = false,
                Message = "Error publishing project"
            };
        }
    }

    public async Task<Response<string>> RollbackAsync(Project project)
    {
        try
        {
            _logger.LogInfo($"Backup initiated for Project : {project.Name}");

            // 1. Create SSH Creds Model
            var credentials = await _serverService.GetServerCredentialsAsync(project.ServerId);
            if (credentials == null) throw new Exception("Server credentials not found");

            // 2. Check if any scripts need to be run BEFORE publishing, if so, run them in order
            var scriptsBeforePublishing = project.Scripts.Where(m => m.RunAfterPublishing == false).ToList();
            await RunScriptsAsync(scriptsBeforePublishing, credentials);

            // 3. Check if publish folder (project name) exists on server, if not throw error
            var publishFolderPath = $"{project.RootDirectory.TrimEnd('/')}/{project.PublishFolder}";

            if (!await _secureShell.DirectoryExists(credentials, publishFolderPath))
            {
                throw new Exception($"Cannot initiate backup, the publish directory for {project.Name} was not found");
            }

            // 4. Check if backup folder exists, if not throw error
            var backupFolderPath = $"{Backup_Prefix}_{publishFolderPath}";
            if (!await _secureShell.DirectoryExists(credentials, backupFolderPath))
            {
                throw new Exception($"Cannot initiate backup, the backup directory bk_{project.PublishFolder} for {project.Name} was not found");
            }

            // 5. Move the files from the backup folder to the publish folder
            await _secureShell.CopyDirectoryContentAsync(credentials, backupFolderPath, publishFolderPath);

            // 6. Check if any scripts need to be run AFTER publishing, if so, run them in order
            var scriptsAfterPublish = project.Scripts.Where(m => m.RunAfterPublishing == true).ToList();
            await RunScriptsAsync(scriptsAfterPublish, credentials);

            _logger.LogInfo($"Backup for {project.Name} deployed successfully");
            return new Response<string> { Message = "Backup successfull" };

        }
        catch (Exception ex)
        {
            _logger.LogError("Error rolling back project", ex);
            return new Response<string>
            {
                Success = false,
                Message = "Error rolling back project"
            };
        }
    }

    private async Task CopyAndReplaceFilesAsync(ServerCredentials credentials, IList<FileStream> fileStreams, string rootDirectory)
    {
        foreach(var file in fileStreams)
        {
            // MAJOR BUG NEED RESOLVING 
            await _secureShell.CopyAndReplaceFileAsync(credentials, file, rootDirectory);
        }
    }
    private async Task RunScriptsAsync(List<Script> scripts, ServerCredentials credentials)
    {
        foreach(var script in scripts)
        {
            await _secureShell.ExecuteScriptAsync(credentials, script);
        }
    }

}
