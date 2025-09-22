using PostBinar.Domain.ProjectMemberships;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Application.Abstractions.Interfaces.Service;

public interface IProjectMembershipService
{
    Task<ProjectMembership> AddMemberAsync(ProjectId projectId, UserId userId);
    Task RemoveMemberAsync(ProjectId projectId, UserId userId);
    Task<IEnumerable<UserId>> GetProjectMemberIdsAsync(ProjectId projectId);

}
