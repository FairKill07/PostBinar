using PostBinar.Domain.ProjectMemberships;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Application.Abstractions.Interfaces.Repositories;

public interface IProjectMembershipRepository
{
    void Add(ProjectMembership membership);
    void Delete(ProjectMembership membership);

    Task<ProjectMembership?> GetMembershipAsync(
        ProjectId projectId, 
        UserId userId,
        CancellationToken cancellationToken = default); 

    Task<IReadOnlyList<ProjectMembership>> GetAllForProjectAsync(
        ProjectId projectId,
        CancellationToken cancellationToken = default);  

    Task<IReadOnlyList<ProjectMembership>> GetAllForUserAsync(
        UserId userId,
        CancellationToken cancellationToken = default);
}
