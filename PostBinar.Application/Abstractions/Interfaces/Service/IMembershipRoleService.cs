using PostBinar.Domain.Enums;
using PostBinar.Domain.ProjectMemberships;

namespace PostBinar.Application.Abstractions.Interfaces.Service;

public interface IMembershipRoleService
{
    Task AssignRoleAsync(ProjectMembershipId membershipId, Role role);
    Task RemoveRoleAsync(ProjectMembershipId membershipId, Role role);
    Task GetRolesForMembershipAsync(ProjectMembershipId projectMembershipId);
}
