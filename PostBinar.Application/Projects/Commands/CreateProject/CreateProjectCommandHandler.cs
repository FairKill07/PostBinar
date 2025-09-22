using MediatR;
using PostBinar.Application.Abstractions.Interfaces.Service;
using PostBinar.Domain.Projects;

namespace PostBinar.Application.Projects.Commands.CreateProject;

public sealed class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ProjectId>
{
    private readonly IProjectService _projectService;
    private readonly IProjectMembershipService _projectMembershipService;
    private readonly IMembershipRoleService _membershipRoleService;
    public CreateProjectCommandHandler(IProjectService projectService, IProjectMembershipService projectMembershipService, IMembershipRoleService membershipRoleService)
    {
        _projectService = projectService;
        _projectMembershipService = projectMembershipService;
        _membershipRoleService = membershipRoleService;
    }
    public async Task<ProjectId> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var projectId = await _projectService.CreateProjectAsync(
            request.Name,
            request.Description,
            request.OwnerId);
        
        var member = await _projectMembershipService.AddMemberAsync(projectId, request.OwnerId);

        await _membershipRoleService.AssignRoleAsync(member.Id, Domain.Enums.Role.Owner);

        return projectId;
    }
}
