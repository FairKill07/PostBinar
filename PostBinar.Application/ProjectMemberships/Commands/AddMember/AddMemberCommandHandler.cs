using MediatR;
using PostBinar.Application.Abstractions.Interfaces.Service;
using PostBinar.Domain.Enums;
using PostBinar.Domain.ProjectMemberships;

namespace PostBinar.Application.ProjectMemberships.Commands.AddMember;

public sealed class AddMemberCommandHandler : IRequestHandler<AddMemberCommand, ProjectMembershipId>
{
    private readonly IProjectMembershipService _projectMembershipService;
    private readonly IMembershipRoleService _membershipRoleService;
    public AddMemberCommandHandler(IProjectMembershipService projectMembershipService, IMembershipRoleService membershipRoleService)
    {
        _projectMembershipService = projectMembershipService;
        _membershipRoleService = membershipRoleService;
    }
    public async Task<ProjectMembershipId> Handle(AddMemberCommand request, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<Role>(request.Role, ignoreCase: true, out var role))
        {
            throw new ArgumentException($"Invalid role: {request.Role}");
        }

        var projectMembership = await _projectMembershipService.AddMemberAsync(request.ProjectId, request.UserId);
        await _membershipRoleService.AssignRoleAsync(projectMembership.Id, role);

        return projectMembership.Id;
    }
}
