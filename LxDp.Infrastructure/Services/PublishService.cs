using LxDp.Application.Interfaces;
using LxDp.Domain;
using LxDp.Domain.DataModels;
using LxDp.Domain.ViewModels;

namespace LxDp.Infrastructure.Services;

public class PublishService : IPublishService
{
    private readonly ISshService _secureShell;
    public PublishService(ISshService secureShell)
    {
        _secureShell = secureShell;
    }
    public Task<Response<string>> PublishAsync(PublishModel request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<string>> RollbackAsync(Project project)
    {
        throw new NotImplementedException();
    }
}
