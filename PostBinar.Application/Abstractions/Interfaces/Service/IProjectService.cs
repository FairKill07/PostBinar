using PostBinar.Domain.Abstraction;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Application.Abstractions.Interfaces.Service;

public interface IProjectService
{
    Task<Result<ProjectId>> CreateProjectAsync(
        string name, 
        string description, 
        UserId ownerId,
        CancellationToken cancellationToken);

    Task<Result<Project>> UpdateProjectAsync(
        UserId ownerId, 
        ProjectId projectId, 
        string name, 
        string description,
        CancellationToken cancellationToken);

    Task<Result> DeleteProject(
        ProjectId projectId,
        CancellationToken cancellationToken);

    Task<Result> Deactivate(
        ProjectId projectId,
        CancellationToken cancellationToken);
}
