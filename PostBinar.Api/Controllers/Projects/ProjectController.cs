using MediatR;
using Microsoft.AspNetCore.Mvc;
using PostBinar.Application.Projects.Commands.CreateProject;
using PostBinar.Application.Projects.Commands.UpdateProject;

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

    [HttpPost]
    public async Task<IActionResult> Update([FromBody] UpdateProjectRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateProjectCommand(
            OwnerId: request.OwnerId,
            ProjectId: request.ProjectId,
            Name: request.Name,
            Description: request.Description
        );
        var project = await _mediator.Send(command, cancellationToken);

        return Ok(project);
    }
}
