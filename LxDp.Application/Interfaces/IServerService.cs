using LxDp.Domain;
using LxDp.Domain.ViewModels;

namespace LxDp.Application.Interfaces;

public interface IServerService
{
    Task<Response<ServerViewModel>> GetServerByIdAsync(int id);
    Task<Response<ServerViewModel>> CreateServerAsync(CreateServerDto request);
    Task<Response<ServerViewModel>> UpdateServerAsync(CreateServerDto request);
    Task<Response<List<ServerViewModel>>> GetAllServersAsync();
    Task<Response<object>> DeleteServerAsync(int id);

}
