using LxDp.Application.Interfaces;
using LxDp.Domain;
using LxDp.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LxDp.Infrastructure.Services;

public class ServerService : IServerService
{
    private readonly AppDbContext _context;
    public ServerService(AppDbContext context)
    {
        _context = context;
    }

    public Task<Response<ServerViewModel>> CreateServerAsync(CreateServerDto request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<object>> DeleteServerAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Response<List<ServerViewModel>>> GetAllServersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Response<ServerViewModel>> GetServerByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Response<ServerViewModel>> UpdateServerAsync(CreateServerDto request)
    {
        throw new NotImplementedException();
    }
}
