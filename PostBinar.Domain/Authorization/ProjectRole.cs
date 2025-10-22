using PostBinar.Domain.Abstraction;
using PostBinar.Domain.Enums;
using PostBinar.Domain.ProjectMemberships;

namespace PostBinar.Domain.Authorization;

public sealed class ProjectRole
{
    private ProjectRole(ProjectMembershipId projectMembershipId, Role role)
    {
        ProjectMembershipId = projectMembershipId;
        RoleId = (int)role;
    }

    protected ProjectRole() { } // EF Core

    public ProjectMembershipId ProjectMembershipId { get; set; }
    public ProjectMembership ProjectMembership { get; set; } = null!;

    public int RoleId { get; set; }
    public RoleEntity Role { get; set; } = null!;

    public static Result<ProjectRole> Create(ProjectMembershipId projectMembershipId, Role role)
    {
        if (projectMembershipId == null || projectMembershipId.Value == Guid.Empty)
            return Result.Failure<ProjectRole>(Error.NullValue);
        
        var projectRole = new ProjectRole
        {
            ProjectMembershipId = projectMembershipId,
            RoleId = (int)role
        };

        return projectRole;
    }
}
