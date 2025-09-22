using LxDp.Domain;
using LxDp.Domain.DataModels;
using LxDp.Domain.ViewModels;

namespace LxDp.Application.Interfaces;

public interface IPublishService
{
    Task<Response<string>> PublishAsync(PublishModel request);
    Task<Response<string>> RollbackAsync(Project project);
}
