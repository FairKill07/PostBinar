using MediatR;
using Microsoft.AspNetCore.Mvc;
using PostBinar.Application.Projects.Commands.CreateProject;
using PostBinar.Application.Projects.Commands.DeleteProject;
using PostBinar.Application.Projects.Commands.UpdateProject;
using PostBinar.Application.Projects.Queries.GetAllProject;
using PostBinar.Application.Projects.Queries.GetProjectById;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

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
            OwnerId: new UserId(request.OwnerId)
        );

        var projectId = await _mediator.Send(command, cancellationToken);
        return Ok(projectId);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateProjectRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateProjectCommand(
            OwnerId: new UserId(request.OwnerId),
            ProjectId: new ProjectId(request.ProjectId),
            Name: request.Name,
            Description: request.Description
        );
        var project = await _mediator.Send(command, cancellationToken);

        return Ok(project);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProjects([FromQuery] Guid userId, CancellationToken cancellationToken)
    {
        var projects = await _mediator.Send(
            new GetAllProjectQuery(new UserId(userId)),
            cancellationToken
        );
        return Ok(projects);
    }

    [HttpGet]
    public async Task<IActionResult> GetProjectById([FromQuery] Guid projectId, CancellationToken cancellationToken)
    {
        var project = await _mediator.Send(
            new GetProjectByIdQuery(new ProjectId(projectId)),
            cancellationToken
        );
        return Ok(project);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteProjectCommand(new ProjectId(id)), cancellationToken);
        return NoContent();
    }


}
