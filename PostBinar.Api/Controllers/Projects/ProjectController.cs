using MediatR;
using Microsoft.AspNetCore.Mvc;
using PostBinar.Application.Projects.Commands.CreateProject;

namespace PostBinar.Api.Controllers.Projects;

public class ProjectController : BaseController
{
    private readonly IMediator _mediator;
    public ProjectController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProjectRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateProjectCommand(
            Name: request.Name,
            Description: request.Description,
            OwnerId: request.OwnerId
        );

        var projectId = await _mediator.Send(command, cancellationToken);
        return Ok(projectId);
    }
}
