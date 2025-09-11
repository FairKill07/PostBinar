using PostBinar.Domain.ProjectMemberships;

namespace PostBinar.Domain.Authorization;

public sealed class ProjectRole
{
    public ProjectMembershipId ProjectMembershipId { get; set; }
    public ProjectMembership ProjectMembership { get; set; } = null!;

    public int RoleId { get; set; }
    public RoleEntity Role { get; set; } = null!;
}
