using LxDp.Application.Interfaces;

namespace LxDp.Infrastructure.Services;

public class LogService : ILogger
{
    private readonly AppDbContext _context;
    public LogService(AppDbContext context)
    {
        _context = context;
    }

    public void LogError(string message, Exception ex)
    {
        throw new NotImplementedException();
    }

    public void LogInfo(string message)
    {
        throw new NotImplementedException();
    }

    public void LogWarning(string message)
    {
        throw new NotImplementedException();
    }
}
