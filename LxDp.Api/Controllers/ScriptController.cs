using LxDp.Application.Commands.Script;
using LxDp.Application.Queries.Script;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LxDp.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ScriptController : ControllerBase
{
    private readonly IMediator _mediator;
    public ScriptController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("name/${name}")]
    public async Task<IActionResult> GetScripts(GetScriptsByNameQuery request)
    {
        var response = await _mediator.Send(request);
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpGet("id/{id}")]
    public async Task<IActionResult> GetScriptById([FromRoute] int id)
    {
        var response = await _mediator.Send(new GetScriptByIdQuery { ScriptId = id});
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpGet("project/{projectId}")]
    public async Task<IActionResult> GetScriptsByProject([FromRoute] int projectId)
     {
        var response = await _mediator.Send(new GetScriptsByProjectQuery { ProjectId = projectId });
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateScript(CreateScriptCommand request)
    {
        var response = await _mediator.Send(request);
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateScript(UpdateScriptCommand request)
    {
        var response = await _mediator.Send(request);
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteScript([FromRoute] int id)
    {
        var response = await _mediator.Send(new DeleteScriptCommand {ScriptId = id});
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

}
