using LxDp.Application.Interfaces;
using LxDp.Domain.DataModels;

namespace LxDp.Infrastructure.Services;

public class LogService : ILogger
{
    private readonly AppDbContext _context;
    private readonly string _logFilePath;

    public LogService(AppDbContext context)
    {
        _context = context;
        var logDir = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
        if (!Directory.Exists(logDir))
            Directory.CreateDirectory(logDir);

        _logFilePath = Path.Combine(logDir, "app-log.txt");
    }

    public void LogError(string message, Exception ex)
    {
        var finalMessage = $"[ERROR] {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} - {message} | Exception: {ex}";
        WriteLog(finalMessage);
    }

    public void LogInfo(string message)
    {
        var finalMessage = $"[INFO] {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} - {message}";
        WriteLog(finalMessage);
    }

    public void LogWarning(string message)
    {
        var finalMessage = $"[WARNING] {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} - {message}";
        WriteLog(finalMessage);
    }

    private void WriteLog(string finalMessage)
    {
        // 1. Write to file
        try
        {
            File.AppendAllText(_logFilePath, finalMessage + Environment.NewLine);
        }
        catch
        {
            // Don't throw logging errors, just swallow
        }

        // 2. Write to database
        try
        {
            var log = new Log
            {
                Timestamp = DateTime.UtcNow,
                Message = finalMessage
            };

            _context.Logs.Add(log);
            _context.SaveChanges();
        }
        catch
        {
            // Swallow to avoid crashing app due to log failure
        }
    }
}
