using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Application.Abstractions.Interfaces.Service;

public interface IProjectService
{
    Task<ProjectId> CreateProjectAsync(string name, string description, UserId ownerId);
    Task<Project> UpdateProjectAsync(UserId ownerId, ProjectId projectId, string name, string description);
    Task DeleteProject(ProjectId projectId);
    Task Deactivate(ProjectId projectId);
}
