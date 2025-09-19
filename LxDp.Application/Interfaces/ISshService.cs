using LxDp.Domain;
using LxDp.Domain.DataModels;

namespace LxDp.Application.Interfaces;

public interface ISshService
{
    Task<Response<object>> InitializeConnection(Server server);
    Task<Response<object>> TerminateConnection();
}
