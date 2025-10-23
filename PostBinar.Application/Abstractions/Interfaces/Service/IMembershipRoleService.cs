using PostBinar.Domain.Abstraction;
using PostBinar.Domain.Enums;
using PostBinar.Domain.ProjectMemberships;

namespace PostBinar.Application.Abstractions.Interfaces.Service;

public interface IMembershipRoleService
{
    Task<Result> AssignRoleAsync(
        ProjectMembershipId membershipId, 
        Role role,
        CancellationToken cancellationToken);

    Task<Result> RemoveRoleAsync(
        ProjectMembershipId membershipId, 
        Role role,
        CancellationToken cancellationToken);

    Task<Result<IEnumerable<ProjectMembership>>> GetRolesForMembershipAsync(
        ProjectMembershipId projectMembershipId,
        CancellationToken cancellationToken);
}
