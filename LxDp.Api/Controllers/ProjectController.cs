using LxDp.Application.Commands.Project;
using LxDp.Application.Queries.Project;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LxDp.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProjectController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("serverId/${serverId}")]
    public async Task<IActionResult> GetAllProjectsByServerId([FromRoute] int serverId)
    {
        var response = await _mediator.Send(new GetAllProjectsQuery { ServerId = serverId});
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpGet("id/{id}")]
    public async Task<IActionResult> GetProjectById([FromRoute] int id)
    {
        var response = await _mediator.Send(new GetAllProjectByIdQuery { Id = id});
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject(CreateProjectCommand request)
    {
        var response = await _mediator.Send(request);
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProject([FromBody] UpdateProjectCommand request)
    {
        var response = await _mediator.Send(request);
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject([FromRoute] int id)
    {
        var response = await _mediator.Send(new DeleteProjectCommand {ProjectId = id});
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }
}
