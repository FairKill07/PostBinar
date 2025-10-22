using PostBinar.Domain.Authorization;
using PostBinar.Domain.ProjectMemberships;

namespace PostBinar.Application.Abstractions.Interfaces.Repositories
{
    public interface IMembershipRoleRepository
    {
        void Add(ProjectRole projectRole);
        void Delete(ProjectRole projectRole);

        Task<ProjectRole> GetByIdAsync(
            ProjectMembershipId projectMembershipId,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<ProjectMembership>> GetRolesForMembershipAsync(
            ProjectMembershipId projectMembershipId,
            CancellationToken cancellationToken = default);
    }
}
