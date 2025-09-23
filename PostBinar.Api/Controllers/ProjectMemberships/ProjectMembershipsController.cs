using MediatR;
using Microsoft.AspNetCore.Mvc;
using PostBinar.Application.ProjectMemberships.Commands.AddMember;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Api.Controllers.ProjectMemberships;

public sealed class ProjectMembershipsController : BaseController
{
    private readonly IMediator _mediator;

    public ProjectMembershipsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> AddMember([FromBody] AddMemberRequest request, CancellationToken cancellationToken)
    {
        var command = new AddMemberCommand(
            ProjectId: new ProjectId(request.ProjectId),
            UserId: new UserId(request.UserId),
            Role: request.Role
        );
        var projectMembershipId = await _mediator.Send(command, cancellationToken);
        return Ok(projectMembershipId);
    }

}
