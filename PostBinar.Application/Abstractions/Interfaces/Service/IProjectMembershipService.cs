using PostBinar.Domain.Abstraction;
using PostBinar.Domain.ProjectMemberships;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Application.Abstractions.Interfaces.Service;

public interface IProjectMembershipService
{
    Task<Result<ProjectMembership>> AddMemberAsync(
        ProjectId projectId,
        UserId userId, 
        CancellationToken cancellationToken);

    Task<Result> RemoveMemberAsync(
        ProjectId projectId, 
        UserId userId, 
        CancellationToken cancellationToken);

    Task<Result<IReadOnlyList<UserId>>> GetProjectMemberIdsAsync(
        ProjectId projectId,
        CancellationToken cancellationToken);

    Task<Result<IReadOnlyList<ProjectMembership>>> GetAllProjectUserAsync(
        UserId userId,
        CancellationToken cancellationToken);
}
