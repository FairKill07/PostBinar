using MediatR;
using PostBinar.Application.Abstractions.Interfaces.Service;
using PostBinar.Domain.Abstraction;
using PostBinar.Domain.Enums;
using PostBinar.Domain.Projects;

namespace PostBinar.Application.Projects.Commands.CreateProject;

public sealed class CreateProjectCommandHandler
    : IRequestHandler<CreateProjectCommand, Result<ProjectId>>
{
    private readonly IProjectService _projectService;
    private readonly IProjectMembershipService _projectMembershipService;
    private readonly IMembershipRoleService _membershipRoleService;

    public CreateProjectCommandHandler(
        IProjectService projectService,
        IProjectMembershipService projectMembershipService,
        IMembershipRoleService membershipRoleService)
    {
        _projectService = projectService;
        _projectMembershipService = projectMembershipService;
        _membershipRoleService = membershipRoleService;
    }

    public async Task<Result<ProjectId>> Handle(
        CreateProjectCommand request,
        CancellationToken cancellationToken)
    {

        var projectResult = await _projectService.CreateProjectAsync(
            request.Name,
            request.Description,
            request.OwnerId,
            cancellationToken);

        if (projectResult.IsFailure)
            return Result.Failure<ProjectId>(projectResult.Error);

        var projectId = projectResult.Value;


        var membershipResult = await _projectMembershipService.AddMemberAsync(
            projectId,
            request.OwnerId,
            cancellationToken);

        if (membershipResult.IsFailure)
            return Result.Failure<ProjectId>(membershipResult.Error);

        var membership = membershipResult.Value;


        var roleResult = await _membershipRoleService.AssignRoleAsync(
            membership.Id,
            Role.Owner,
            cancellationToken);

        if (roleResult.IsFailure)
            return Result.Failure<ProjectId>(roleResult.Error);

        return Result.Success(projectId);
    }
}
