using LxDp.Application.Commands.Server;
using LxDp.Application.Commands.User;
using LxDp.Application.Queries.Server;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LxDp.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServerController : ControllerBase
{
    private readonly IMediator _mediator;
    public ServerController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllServers(GetAllServersQuery request)
    {
        var response = await _mediator.Send(request);
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpGet("id/{id}")]
    public async Task<IActionResult> GetServerById([FromRoute] int id)
    {
        var response = await _mediator.Send(new GetServerByIdQuery { Id = id});
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateServer(CreateServerCommand request)
    {
        var response = await _mediator.Send(request);
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpPost("user")]
    public async Task<IActionResult> CreateServerUser(CreateServerUserCommand request)
    {
        var response = await _mediator.Send(request);
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateServer(UpdateServerCommand request)
    {
        var response = await _mediator.Send(request);
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateServerUser(UpdateServerUserCommand request)
    {
        var response = await _mediator.Send(request);
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteServer([FromRoute] int id)
    {
        var server = await _mediator.Send(new DeleteServerCommand { ServerId = id });
        if (!server.Success)
        {
            return BadRequest(server);
        }
        return Ok(server);
    }
    [HttpDelete("user/{id}")]
    public async Task<IActionResult> DeleteServeruser([FromRoute] int id)
    {
        var server = await _mediator.Send(new DeleteUserCommand { UserId = id });
        if (!server.Success)
        {
            return BadRequest(server);
        }
        return Ok(server);
    }
}
