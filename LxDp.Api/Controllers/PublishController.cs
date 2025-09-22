using LxDp.Application.Commands.Publish;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LxDp.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PublishController : ControllerBase
{
    private readonly IMediator _mediator;
    public PublishController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<IActionResult> Publish([FromBody] PublishCommand request)
    {
        var response = await _mediator.Send(request);
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpPost("backup")]
    public async Task<IActionResult> InitiateBackup([FromBody] InitiateBackupCommand request)
    {
        var response = await _mediator.Send(request);
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }
}
